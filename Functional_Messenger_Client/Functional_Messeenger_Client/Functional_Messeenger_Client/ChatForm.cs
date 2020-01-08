using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;


namespace Functional_Messeenger_Client
{ //나가기버튼 추가해야함
    public partial class ChatForm : Form
    {
        private TcpClient client = null;
        private NetworkStream N_stream = null;
        private Quary quary = null;
        bool isMove = false;
        Point fpt = new Point();
        shareControl shareControler = new shareControl();
        bool flag_ = false;
        List<string> UserArray = null;
        public string id{
            get;set;
        }
        public string userArray
        {
            get;set;
        }
        public ChatForm(string id,string userArray)
        {
            InitializeComponent();
            this.id = id; //내 아이디
            this.userArray = userArray; //채팅하는 사람들의 아이디
            this.flag_ = false;
            UserArray = (userArray.Split(',')).ToList<string>();
            quary = new Quary();
            Font tFont = new Font(FontBox.externalFont.Families[3], 11);
            Font bFont = new Font(FontBox.externalFont.Families[3], 11);
            textBox.Font = tFont;
            ChatBox.Font = tFont;
            Sharing_button.Font = bFont;
        }
        public ChatForm(string id, string userArray,bool Flag)
        {
            InitializeComponent();
            this.id = id; //내 아이디
            this.userArray = userArray; //채팅하는 사람들의 아이디
            this.flag_ = Flag;
            UserArray = (userArray.Split(',')).ToList<string>();
            quary = new Quary();
            Font tFont = new Font(FontBox.externalFont.Families[3], 11);
            Font bFont = new Font(FontBox.externalFont.Families[3], 11);
            textBox.Font = tFont;
            ChatBox.Font = tFont;
            Sharing_button.Font = bFont;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect("192.168.35.191", 9999);
            N_stream = client.GetStream();
            string linkingArray_with_id = id + "+" + userArray;
            byte[] MyId = Encoding.Default.GetBytes(linkingArray_with_id);
            N_stream.Write(MyId, 0, MyId.Length);
            Receive_Message();
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            string To = "[@";
            foreach(string user in UserArray)
            {
                To += user + "@";
            }
            To += "]";
            byte[] data = System.Text.Encoding.Default.GetBytes("~("+textBox.Text+To+")~");
            N_stream.Write(data, 0, data.Length);
            N_stream.Flush();
            textBox.Clear();
        }
        private async void Receive_Message()
        {
            bool flag = false;
            bool keep = false;
            int Len;
            byte[] Stream_data = new byte[1024];
            string message = null;
            if (flag_ == true)
            {
                userlistUpdator_Click(this, new EventArgs());
            }
            this.flag_ = false;
            try
            {
                await Task.Run(() =>
                {
                    while (true)
                    {
                        //flag = false;
                        //keep = false;
                        if (N_stream.CanRead)
                        {
                            Len = N_stream.Read(Stream_data, 0, Stream_data.Length); //비동기 코드
                            message = Encoding.Default.GetString(Stream_data, 0, Len);
                           
                            if (message.Equals(Packet.Shutdown))
                            {
                                ChatBox.Invoke((MethodInvoker)delegate () {
                                    ChatBox.AppendText("서버와의 연결이 끊어졌습니다.\n");
                                });
                                N_stream.Close();
                                client.Close();
                                goto EXIT;
                            }
                            else if (message.StartsWith("[$") && message.EndsWith("$]"))
                            {
                                List<string> Date_and_Text = message.Split('$').ToList<string>();
                                Date_and_Text.RemoveAt(0);
                                Date_and_Text.RemoveAt(Date_and_Text.Count - 1);//'[', ']'제거
                                if (MessageBox.Show("일정이 왔습니다.\n일정 : " +Date_and_Text[0]+ "\n내용 : "+Date_and_Text[1], "공유알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    int checkNum = shareControler.ReceiveShareSchedule(id, Date_and_Text);
                                    if (checkNum == 0)
                                    {
                                        MessageBox.Show("일정이 수정되었습니다.");
                                    }
                                    else
                                    {
                                        MessageBox.Show("일정이 삽입되었습니다.");
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if (message.Equals(Packet.Approve))
                            {
                                ChatBox.Invoke((MethodInvoker)delegate ()
                                {
                                    ChatBox.AppendText("접속성공!\n");
                                });
                            }
                            else if (message.StartsWith("$$!") && message.EndsWith("!$$"))
                            {
                                string[] RealMessage = message.Split('!');
                                string ExitName = (RealMessage[1].Split('[')[1]).Split(']')[0];
                                UserArray.Remove(ExitName);
                                ChatBox.Invoke((MethodInvoker)delegate ()
                                {
                                    ChatBox.AppendText(RealMessage[1] + "\n");
                                });
                            }
                            else if (message.StartsWith("<@") && message.EndsWith("@>"))//UserList업데이트
                            {
                                string[] targetuser = message.Split('@');
                                string[] Realtargetuser = targetuser[1].Split(','); //채팅할 유저
                                List<string> box = new List<string>();
                                foreach(string Tuser in UserArray) //기존 가진 유저
                                {
                                    keep = false;
                                    for(int i = 0; i < Realtargetuser.Length; i++)
                                    {
                                        if (Tuser.Equals(Realtargetuser[i]))
                                            keep = true;
                                    }
                                    if (keep == false)
                                        box.Add(Tuser);
                                }
                                foreach (string element in box)
                                    UserArray.Remove(element);
                            }
                            else if (message.Length >= 1) //한 글자 이상이라면,
                            {
                                string fromID = (message.Split(':')[0]).Split(' ')[0]; //어떤 아이디로부터 온것인가?
                                foreach (string target in UserArray) {
                                    if (target.Equals(fromID)) //내가 채팅하는 사람들의 아이디중 하나와 일치한다.
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (flag) //일치하면 내 채팅창에 메세지를 출력하라.
                                {
                                    ChatBox.Invoke((MethodInvoker)delegate ()
                                    {
                                        ChatBox.AppendText(message + "\n");
                                    });
                                }
                                flag = false;
                            }
                        }
                    }
                EXIT:;
                });
            }
            catch (SocketException errcode)
            {
                MessageBox.Show(errcode.Message);
            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            string To = "@";
            foreach (string user in UserArray)
            {
                To += user + "@";
            }
            byte[] data = System.Text.Encoding.Default.GetBytes(Packet.Shutdown+To);
            N_stream.Write(data, 0, data.Length);
            DupleChecker.isOpen = false;
        }

        private void Sharing_button_Click(object sender, EventArgs e)
        {
            string scheduleText = null;
            string To = "@";
            foreach (string user in UserArray)
            {
                To += user + "@";
            }
            scheduleText = shareControler.request_getText(id, Myschedule_date.Value.ToString("yyyy-MM-dd"));
            if (scheduleText.Equals("")) {
                MessageBox.Show("보낼 일정이 없습니다.");
                return;
            }
            byte[] data = System.Text.Encoding.Default.GetBytes("[$"+ Myschedule_date.Value.ToString("yyyy-MM-dd") + "$"+
                scheduleText+"$]"+To);
            N_stream.Write(data, 0, data.Length);
            N_stream.Flush();      
        }

        private void Panel_down(object sender, MouseEventArgs e)
        {
            isMove = true;
            fpt = new Point(e.X, e.Y);
        }

        private void Panel_move(object sender, MouseEventArgs e)
        {
            if (isMove && (e.Button & MouseButtons.Left) == MouseButtons.Left)
                this.Location = new Point(this.Left - (fpt.X - e.X), this.Top - (fpt.Y - e.Y));
        }

        private void Panel_up(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userlistUpdator_Click(object sender, EventArgs e)
        {
            byte[] TargetUser = Encoding.Default.GetBytes("<@" + userArray + "@>");
            N_stream.Write(TargetUser, 0, TargetUser.Length);
        }
    }
}
