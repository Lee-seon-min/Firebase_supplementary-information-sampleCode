using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using MySql.Data.MySqlClient;

namespace Functionality_Messenger_Server
{
    public partial class HomeServer : Form
    {
        string URL = "server=127.0.0.1; database=messanger; user=mclient; password=mc123;"; //데이터베이스 계정과 사용 DB선언
        const int PORT = 10000; //포트번호를 다르게 해서 서버사이드를 하나더 만들자! 코드는 같은거니까
        static int serverCount = 1; //서버는 한개만 개설이 가능
        TcpListener server = null; //서버객체
        TcpClient client = null; //클라이언트 객체
        Dictionary<TcpClient, string> List_of_client = null; //연결되있는 클라이언트들의 리스트

        Thread Acceptor = null; //서버구동 쓰레드
        MySqlConnection connection = new MySqlConnection();
        MySqlConnection connection2 = new MySqlConnection(); //이중연결의 필요
        MySqlCommand command = new MySqlCommand();
        MySqlCommand command2 = new MySqlCommand(); //이중연결의 필요
        MySqlDataReader reader = null;
        public HomeServer()
        {
            InitializeComponent();
            List_of_client = new Dictionary<TcpClient, string>(); //클라이언트 박스 생성
            Acceptor = new Thread(RequestingClient_Acceptor); //서버구동 쓰레드 시작
            Acceptor.IsBackground = true; //백그라운드로 쓰레딩
        }
        private string get_ComputerIP() //현재 서버pc의 ip반환
        {
            IPHostEntry computer = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress IP in computer.AddressList)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IP.ToString();
                }
            }
            return null;
        }

        private void HomeServer_Load(object sender, EventArgs e)
        {
            connection.ConnectionString = URL; //연결은 URL정보로
            command.Connection = connection; //이 명령어는 위 커넥션정보로 연결됩니다.
            connection2.ConnectionString = URL;
            command2.Connection = connection2;
            IPAddress adress = new IPAddress(0); //서버IP 저장
            server = new TcpListener(adress, PORT); //서버의 IP와 포트번호 부여
            try
            {
                connection.Open();
                connection2.Open();
                command.CommandText = "update userlist set status='red'";
                command.ExecuteNonQuery();
                command.CommandText = "select ID from userlist";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    command2.CommandText = "update " + reader["ID"] + "_friends set status='red'";
                    command2.ExecuteNonQuery();
                }
            }
            finally
            {
                reader.Close();
                connection2.Close();
                connection.Close();
                server.Stop();
            }
        }
        private void Listen_Start()
        {
            server.Start(); //서버 수신 시작
            serverCount--; //서버는 더이상 재생성할수 없음.
        }
        private delegate void ClientThreadHandler();
        private async void RequestingClient_Acceptor()
        {
            client = default(TcpClient); //클라이언트 생성
            string name = null; //해당 연결된 클라이언트의 id를 저장
            byte[] message = null; //패킷을 담을 공간
            bool isDuple = false;
            await Task.Run(() => //비동기적 실행(await)
            {
                while (true)
                {
                    client = server.AcceptTcpClient();
                    statusBox.Invoke((MethodInvoker)delegate()
                    {
                        statusBox.AppendText("Try Approve Client Access...\n");
                    });

                    NetworkStream stream = client.GetStream(); //해당 클라이언트와 네트워크 스트림 생성
                    byte[] Stream_data = new byte[1024];
                    int Len = stream.Read(Stream_data, 0, Stream_data.Length); //연결되면 클라이언트는 id를 보낸다.
                    name = Encoding.Default.GetString(Stream_data, 0, Len); //string형식으로 인코딩합니다.
                    stream.Flush();

                    message = Encoding.Default.GetBytes(Packet.Approve); //연결 허가 패킷제작
                    stream.Write(message, 0, message.Length); //위 패킷 전송
                    stream.Flush();

                    List_of_client.Add(client, name); //클라이언트 객체와 id저장

                    HomeClientAction Branch_client = new HomeClientAction(client, name); //서버사이드 쓰레드의 생성
                    Branch_client.Receive_Action += new HomeClientAction.MessageReceive_Throw(Received_Message); //이벤트 함수추가1
                    Branch_client.Shutdown_Action += new HomeClientAction.ShutdownCode_Throw(ExitServer); //이벤트 함수추가2
                    ClientThreadHandler RunningMachine = new ClientThreadHandler(Branch_client.Start_Thread_of_Client);
                    BeginInvoke(RunningMachine);
                    statusBox.Invoke((MethodInvoker)delegate ()
                    {
                        statusBox.AppendText($"{name} Login...\n");
                    });
                   
                    try
                    {
                        connection.Open();
                        connection2.Open();
                        command.CommandText = "update userlist set status='green' where ID='" + name + "'"; //userlist의 해당 아이디상태 그린으로 업데이트
                        command.ExecuteNonQuery(); //위 쿼리문 실행
                        command.CommandText = "select ID from userlist"; //그리고 유저리스트의 id모두 찾아서
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            command2.CommandText = "update " + reader["ID"].ToString() + "_friends set status='green' where ID='" + name + "'"; //그 아이디의 친구목록에서 해당 아이디상태 변경
                            command2.ExecuteNonQuery();
                        }
                        Task.Run(()=> {
                            foreach (var User in List_of_client)
                            {
                                TcpClient client = User.Key;
                                stream = client.GetStream();
                                byte[] Stream_data_ = Encoding.Default.GetBytes(Packet.ChangeSignal); //테이블의 상태변화 되었음을 알림

                                stream.Write(Stream_data_, 0, Stream_data_.Length);
                                stream.Flush();
                            }
                        });
                       
                    }
                    catch (Exception err) { }
                    finally
                    {
                        reader.Close();
                        connection.Close();
                        connection2.Close();
                    }
                }
            });
        }
        private async void Received_Message(string Origin_message, string name) //이벤트
        {
            NetworkStream stream = null;
            int count = 0;
            if (Origin_message.Equals(Packet.LogoutSignal))
            {
                await Task.Run(() =>
                {
                    foreach (string processID in List_of_client.Values) {
                        if (processID.Equals(name))
                            count++;
                    }
                    if (count >= 2)
                        return; //중복로그인이면 status 변경x
                    try
                    {
                        connection.Open();
                        connection2.Open();
                        command.CommandText = "update userlist set status='red' where ID='" + name + "'"; //userlist의 해당 아이디상태 레드로 업데이트
                        command.ExecuteNonQuery();
                        command.CommandText = "select ID from userlist"; //그리고 유저리스트의 id모두 찾아서
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            command2.CommandText = "update " + reader["ID"].ToString() + "_friends set status='red' where ID='" + name + "'"; //그 아이디의 친구목록에서 해당 아이디상태 변경
                            command2.ExecuteNonQuery();
                        }
                    }
                    catch (Exception err) { }
                    finally
                    {
                        reader.Close();
                        connection.Close();
                        connection2.Close();
                    }
                    return;
                });
            }
            else if (Origin_message.StartsWith("[&") && Origin_message.EndsWith("&]"))
            {
                await Task.Run(() =>
                {
                    statusBox.Invoke((MethodInvoker)delegate ()
                    {
                        statusBox.AppendText("Receive Data Format '" + Origin_message + "'\n");
                    });
                    string[] userList = Origin_message.Split('&');
                    List<string> uList = new List<string>();
                    for (int i = 2; i < userList.Length - 1; i++)
                        uList.Add(userList[i]);
                    foreach (var User in List_of_client)
                    {
                        foreach (string user in uList)
                        {
                            if (user.Equals(User.Value))
                            {
                                TcpClient client = User.Key;
                                stream = client.GetStream();
                                byte[] Stream_data_ = Encoding.Default.GetBytes(Origin_message); //해당 id에게 전송

                                stream.Write(Stream_data_, 0, Stream_data_.Length);
                                stream.Flush();
                            }
                        }
                    }
                });
            }
            else if (Origin_message.StartsWith("^#") && Origin_message.EndsWith("#^"))
            {
                string devideCharp = Origin_message.Split('#')[1];
                string Clicker = devideCharp.Split('*')[1]; //채팅시작을 클릭한 사람
                statusBox.Invoke((MethodInvoker)delegate ()
                {
                    statusBox.AppendText("Receive Data Format '" + Origin_message + "'\n");
                    statusBox.AppendText("Clicker : '" + Clicker + "'\n");
                });
                foreach (var User in List_of_client)
                {
                    if (User.Value.Equals(Clicker))
                    {
                        TcpClient client = User.Key;
                        stream = client.GetStream();
                        byte[] Stream_data_ = Encoding.Default.GetBytes(Origin_message); //해당 id에게 전송

                        stream.Write(Stream_data_, 0, Stream_data_.Length);
                        stream.Flush();
                    }
                }
            }
        }
        private void ExitServer(TcpClient target, string name)
        {
            NetworkStream stream = null;
            stream = target.GetStream();
            byte[] ExitCode = Encoding.Default.GetBytes(Packet.Shutdown);
            stream.Write(ExitCode, 0, ExitCode.Length);
            stream.Flush();
            ExitCode = Encoding.Default.GetBytes(Packet.ChangeSignal);
            statusBox.Invoke((MethodInvoker)delegate ()
            {
                statusBox.AppendText($"{name} Logout... \n");
            });
            List_of_client.Remove(target);
            foreach (var User in List_of_client)
            {
                TcpClient client = User.Key;
                stream = client.GetStream();

                stream.Write(ExitCode, 0, ExitCode.Length);
                stream.Flush();
            }
        }
        private void ListenButton_Click(object sender, EventArgs e)
        {
            Listen_Start();
            statusBox.Invoke((MethodInvoker)delegate ()
            {
                statusBox.AppendText($"서버가 개설되었습니다.. (serverIP : '{get_ComputerIP()}')\n");
            });
            
            Acceptor.Start();
        }

        private void ServerClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                connection.Open();
                connection2.Open();
                command.CommandText = "update userlist set status='red'";
                command.ExecuteNonQuery();
                command.CommandText = "select ID from userlist";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    command2.CommandText = "update " + reader["ID"] + "_friends set status='red'";
                    command2.ExecuteNonQuery();
                }
            }
            finally
            {
                reader.Close();
                connection2.Close();
                connection.Close();
                server.Stop();
            }
        }
    }
}
