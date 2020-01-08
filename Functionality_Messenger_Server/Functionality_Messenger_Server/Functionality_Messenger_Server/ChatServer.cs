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
    public partial class ChatServer : Form
    {
        const int PORT = 9999; //이거 다르게 해서 서버사이드를 하나더 만들자! 코드는 같은거니까
        static int serverCount = 1;
        TcpListener server = null;
        TcpClient client = null;
        Dictionary<TcpClient, string> List_of_client = null; //연결되있는 클라이언트들의 리스트
        Thread Acceptor = null;

        public ChatServer()
        {
            InitializeComponent();
            List_of_client = new Dictionary<TcpClient, string>();
            Acceptor = new Thread(RequestingClient_Acceptor);
            Acceptor.IsBackground = true;
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
        private void Form1_Load(object sender, EventArgs e)
        {
            IPAddress adress = new IPAddress(0); //서버IP 저장
            server = new TcpListener(adress, PORT);
        }
        private void Listen_Start()
        {
            server.Start();
            serverCount--;
        }
        private delegate void ClientThreadHandler();
        private async void RequestingClient_Acceptor() {
            client = default(TcpClient); //클라이언트 생성
            string name = null;
            byte[] message = null;
            await Task.Run(() =>
            {
                //내가 선택한 아이디에서 채팅방 들어오라고 질의는 어떻게 할까??

                while (true) {
                    client = server.AcceptTcpClient();
                    StatusBox.Invoke((MethodInvoker)delegate ()
                    {
                        StatusBox.AppendText("Try Approve Client Access...\n");
                    });

                    NetworkStream stream = client.GetStream();
                    
                    byte[] Stream_data = new byte[1024];
                    int Len = stream.Read(Stream_data, 0, Stream_data.Length); //연결되면 클라이언트는 id+userlist를 보낸다.
                    string DataFormat = Encoding.Default.GetString(Stream_data, 0, Len); //string형식으로 인코딩합니다.
                    name = DataFormat.Split('+')[0];
                    
                    stream.Flush();
                    message= Encoding.Default.GetBytes(Packet.Approve);
                    stream.Write(message, 0, message.Length);
                    stream.Flush();
                   
                    List_of_client.Add(client, name); //클라이언트 객체와 id저장

                    ChatClientAction Branch_client = new ChatClientAction(client,name); //UserString : user1,user2,user3...
                    Branch_client.Receive_Action += new ChatClientAction.MessageReceive_Throw(Received_Message);
                    Branch_client.Shutdown_Action += new ChatClientAction.ShutdownCode_Throw(ExitServer);
                    ClientThreadHandler RunningMachine = new ClientThreadHandler(Branch_client.Start_Thread_of_Client);
                    BeginInvoke(RunningMachine);
                    
                }
            });
        }
        private async void Received_Message(string Origin_message,string S_message, string name,string users) //이벤트
        {//name=나의 아이디, users= myid,user1,user2
            NetworkStream stream = null;
            if (Origin_message.Equals(Packet.Shutdown))
            {
                StatusBox.Invoke((MethodInvoker)delegate ()
                {
                    StatusBox.AppendText(S_message + "\n");
                });

                return;
            }
            if(Origin_message.StartsWith("[$")&&Origin_message.EndsWith("$]"))
            {
                StatusBox.Invoke((MethodInvoker)delegate ()
                {
                    StatusBox.AppendText("Receive Data Format : " + Origin_message + "\nusers : " + users+"\n");
                });
                List<string> targetUser = users.Split(',').ToList<string>();
               
                await Task.Run(() =>
                {
                    foreach (var User in List_of_client)
                    {
                        foreach (string inuser in targetUser)
                        {
                            if (inuser.Equals(User.Value))
                            {
                                TcpClient client = User.Key;
                                stream = client.GetStream();
                                byte[] Stream_data = Encoding.Default.GetBytes(Origin_message);

                                stream.Write(Stream_data, 0, Stream_data.Length);
                                stream.Flush();
                            }
                        }
                    }
                });
                return;
            }
            if (Origin_message.StartsWith("<@") && Origin_message.EndsWith("@>")) {
                StatusBox.Invoke((MethodInvoker)delegate ()
                {
                    StatusBox.AppendText("Receive Data Format : " + Origin_message + "\nusers : "+users+"\n");
                });
                List<string> targetUser = users.Split(',').ToList<string>();
                await Task.Run(() =>
                {
                    foreach (var User in List_of_client)
                    {
                        foreach (string inuser in targetUser)
                        {
                            if (inuser.Equals(User.Value))
                            {
                                TcpClient client = User.Key;
                                stream = client.GetStream();
                                byte[] Stream_data = Encoding.Default.GetBytes(Origin_message);

                                stream.Write(Stream_data, 0, Stream_data.Length);
                                stream.Flush();
                            }
                        }
                    }
                });
                return;
            }
            if (Origin_message.Length >= 1)
            {
                string[] userBox = null;
          
                StatusBox.Invoke((MethodInvoker)delegate ()
                {
                    StatusBox.AppendText(S_message +" to "+users+"\n");
                });
                await Task.Run(() =>
                {
                    userBox = users.Split(',');
                    List<string> list = userBox.ToList<string>();
                    foreach (var User in List_of_client)
                    {
                        foreach (string inuser in list)
                        {
                            if (inuser.Equals(User.Value))
                            {
                                TcpClient client = User.Key;
                                stream = client.GetStream();
                                byte[] Stream_data = Encoding.Default.GetBytes(name + " : " + Origin_message);

                                stream.Write(Stream_data, 0, Stream_data.Length);
                                stream.Flush();
                            }
                        }
                    }
                });
            }
            else return;
        }
        private void ExitServer(TcpClient target,string name,string users)
        {
            NetworkStream stream = null;
            //stream = target.GetStream();
            //byte[] ExitCode_ = Encoding.Default.GetBytes(Packet.Shutdown);
            //stream.Write(ExitCode_, 0, ExitCode_.Length);
            //stream.Flush();
            List_of_client.Remove(target);

            string[] userBox = users.Split(',');
            List<string> list = userBox.ToList<string>();
            foreach (var User in List_of_client)
            {
                foreach (string inuser in list)
                {
                    if (inuser.Equals(User.Value))
                    {
                        TcpClient client = User.Key;
                        stream = client.GetStream();
                        byte[] Stream_data = Encoding.Default.GetBytes("$$![" + name + "]님이 접속을 해제하였습니다.!$$");

                        stream.Write(Stream_data, 0, Stream_data.Length);
                        stream.Flush();
                    }
                }
            }
        }
        private void ListenButton_Click(object sender, EventArgs e)
        {
            Listen_Start();
            StatusBox.AppendText($"서버가 개설되었습니다.. (serverIP : '{get_ComputerIP()}')\n");
            Acceptor.Start();
        }
    }
}
