using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Functional_Messeenger_Client
{
    public partial class Home : Form
    {
        addFriendControl addFriendControler = new addFriendControl();
        progressControl progressControler = new progressControl();
        ChatForm chatForm = null; //채팅하기 누르면 할당
        Calender cal = null;
        bool isMove=false; //패널전용
        Point fpt = new Point();
        Quary quary = new Quary(); //DB전용
        private TcpClient client = null;
        private NetworkStream N_stream = null;
        FriendsListBox friendsListBox = null;
        Action RunChatClient = null;
        int count = 0;
        int max = 0;
        string selectedUser = null;
        List<string> selectedUserArray = new List<string>();
        Task<DialogResult> process_Chat = null;

        public string id
        {
            get; set;
        }
        public Home(string id)
        {
            InitializeComponent();
            this.id = id;
            friendsListBox = new FriendsListBox(id);
            friendsListBox.Size = new Size(285, 150);
            friendsListBox.Location = new Point(27, 420);
            friendsListBox.BorderStyle = BorderStyle.None;
            RunChatClient = new Action(RunChatting);
            this.Controls.Add(friendsListBox);
            FontSet();
        }
        private void FontSet()
        {
            Font DdayFont = new Font(FontBox.externalFont.Families[0], 15);
            Font AddboxFont = new Font(FontBox.externalFont.Families[0], 15);
            Font Time_sub_Font = new Font(FontBox.externalFont.Families[0], 9);
            Font Sub_Font = new Font(FontBox.externalFont.Families[0], 12);
            Font FriendbuttonFont = new Font(FontBox.externalFont.Families[3], 10);
            Font FriendListFont = new Font(FontBox.externalFont.Families[1], 12);
            Dday_label.Font = DdayFont;
            Dday_label2.Font = DdayFont;
            start_time.Font = Time_sub_Font;
            start_time2.Font = Time_sub_Font;
            end_time.Font = Time_sub_Font;
            end_time2.Font = Time_sub_Font;
            subject.Font = Sub_Font;
            subject2.Font = Sub_Font;
            Addfriend.Font = Time_sub_Font;
            UpdateFriendsList.Font = Time_sub_Font;
            friendsListBox.Font = FriendListFont;
            button1.Font = FriendbuttonFont;
            button2.Font = FriendbuttonFont;
            friendIDbox.Font = AddboxFont;
            Open_calender_button.Font = Time_sub_Font;
            subject_box.Font = Sub_Font;
            subject_box2.Font = Sub_Font;

        }
        private void Home_Load(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect("192.168.35.191", 10000);
            N_stream = client.GetStream();
            byte[] MyId = Encoding.Default.GetBytes($"{id}");
            N_stream.Write(MyId, 0, MyId.Length);
            friendsListBox.Change_ShowState();
            get_schedule();
            Action RECEIVE = new Action(Receive_Message);
            BeginInvoke(RECEIVE);
        }
        private async void Receive_Message()
        {
            
            bool flag = false;
            int Len;
            byte[] Stream_data = new byte[1024];
            string message = null;
            string spliting_user = null;
            try
            {
                await Task.Run( () =>
                {
                    while (true)
                    {
                        flag = false;
                        if (N_stream.CanRead && N_stream != null)
                        {
                            Len = N_stream.Read(Stream_data, 0, Stream_data.Length); //비동기 코드
                            message = Encoding.Default.GetString(Stream_data, 0, Len);
                            if (message.Equals(Packet.Shutdown))
                            {
                                N_stream.Close();
                                client.Close();
                                N_stream = null;
                                client = null;
                                goto EXIT;
                            }
                            else if (message.Equals(Packet.Approve))
                            {
                                MessageBox.Show("환영합니다.");
                            }
                            else if (message.Equals(Packet.ChangeSignal))//상태변화
                            {
                                MessageBox.Show("Updating FriendsList...");
                                UpdateFriendsList_Click(this, new EventArgs());
                            }
                            else if (message.StartsWith("[&") && message.EndsWith("&]"))
                            {
                                string[] userArray = message.Split('&');//userArray[1]=채팅시작한 얘
                                spliting_user = null;
                                for (int i = 1; i < userArray.Length - 1; i++)
                                {
                                    spliting_user += userArray[i];
                                    if (!(i == userArray.Length - 2))
                                        spliting_user += ",";
                                    if (this.id.Equals(userArray[i]))
                                        flag = true;

                                }
                                if (flag == true)
                                {
                                    if (MessageBox.Show("USER : "+spliting_user + "\n채팅을 시작하시겠습니까?", "채팅알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        if (DupleChecker.isOpen)
                                        {
                                            string DataFormating = "^#" + this.id + "*" + userArray[1] + "*" + Packet.ChatResponse_ok + "#^";
                                            byte[] myOpinion = Encoding.Default.GetBytes(DataFormating);
                                            N_stream.Write(myOpinion, 0, myOpinion.Length);
                                            chatForm.Close();
                                            chatForm = null;
                                            chatForm = new ChatForm(id, spliting_user);//내 아이디랑 채팅하는 사람들의 id
                                            Func<DialogResult> DoChat = new Func<DialogResult>(chatForm.ShowDialog);
                                            process_Chat = new Task<DialogResult>(DoChat);
                                            DupleChecker.isOpen = true;
                                            process_Chat.Start();
                                        }
                                        else
                                        {
                                            string DataFormating = "^#" + this.id + "*" + userArray[1] + "*" + Packet.ChatResponse_ok + "#^";
                                            byte[] myOpinion = Encoding.Default.GetBytes(DataFormating);
                                            N_stream.Write(myOpinion, 0, myOpinion.Length);
                                            DupleChecker.isOpen = true;
                                            chatForm = new ChatForm(id, spliting_user);//내 아이디랑 채팅하는 사람들의 id
                                            Func<DialogResult> DoChat = new Func<DialogResult>(chatForm.ShowDialog);
                                            process_Chat = new Task<DialogResult>(DoChat);
                                            process_Chat.Start();
                                          
                                        }
                                    }
                                    else
                                    {//거부의사
                                        string DataFormating = "^#" + this.id + "*" + userArray[1] + "*" + Packet.ChatResponse_no + "#^";
                                        byte[] myOpinion = Encoding.Default.GetBytes(DataFormating);
                                        N_stream.Write(myOpinion, 0, myOpinion.Length);
                                    }
                                }
                            }
                            else if (message.StartsWith("^#") && message.EndsWith("#^"))
                            {
                                string devideCharp = message.Split('#')[1];
                                string responser = devideCharp.Split('*')[0]; //응답한 사람
                                string responsePack = devideCharp.Split('*')[2];
                                for (int i = 0; i < selectedUserArray.Count; i++) //채팅하고자 하는 사람들 중
                                {
                                    if (selectedUserArray[i].Equals(responser) && responsePack.Equals(Packet.ChatResponse_no))
                                    {
                                        selectedUserArray.Remove(responser);
                                    }
                                }

                                count++;
                                if (count == max)
                                {
                                    int j = 0;
                                    int sum = 0;
                                    spliting_user = this.id;
                                    for (int i = 1; i < selectedUserArray.Count; i++)
                                    {
                                        spliting_user += "," + selectedUserArray[i];
                                    }
                                    DupleChecker.isOpen = true;
                                    chatForm = new ChatForm(id, spliting_user,true);//내 아이디랑 채팅하는 사람들의 id
                                    while (j <= 200)
                                    {
                                        sum += j;
                                        j++;
                                    }//딜레이
                                    Func<DialogResult> DoChat = new Func<DialogResult>(chatForm.ShowDialog);
                                    Task<DialogResult> process_Chat = new Task<DialogResult>(DoChat);
                                    process_Chat.Start();
                                    count = 0;
                                    max = 0;
                                }
                            }
                        }
                    }
                EXIT: MessageBox.Show("turn off the HomeForm...");
                    this.Close();
                });
            }
            catch (SocketException errcode)
            {
                MessageBox.Show(errcode.Message);
            }
        }
        [Obsolete("더 이상 사용하지 않는 함수입니다.")]
        public void RunChatting()
        {
            chatForm.ShowDialog();
        }
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chatForm != null)
                chatForm.Close();
            if (cal != null)
                cal.Close();
            byte[] MyId = Encoding.Default.GetBytes(Packet.LogoutSignal);
            N_stream.Write(MyId, 0, MyId.Length);
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            if (DupleChecker.isOpen)
            {
                MessageBox.Show("아직 채팅중입니다.");
                return;
            }
            /*byte[] MyId = Encoding.Default.GetBytes(Packet.LogoutSignal);
            N_stream.Write(MyId, 0, MyId.Length);*/
            this.Close();
        }
        [Obsolete("사용하지않는 함수")]
        private void Calender_Click(object sender, EventArgs e)
        {
            //
        }

        private void UpdateFriendsList_Click(object sender, EventArgs e)
        {
            friendsListBox.Change_ShowState();
        }

        private void Addfriend_Click(object sender, EventArgs e)
        {
            int checkNum = addFriendControler.Checkfriend(friendIDbox.Text, id);
            if (checkNum == -1)
            {
                MessageBox.Show("자기자신을 추가할수는 없습니다.");
                friendIDbox.Clear();
            }
            else if (checkNum == 0)
            {
                MessageBox.Show("이미 등록한 친구입니다.");
                friendIDbox.Clear();
            }
            else if (checkNum == 1)
            {
                MessageBox.Show("존재하지 않는 ID입니다.");
                friendIDbox.Clear();
            }
            else if (checkNum == 2)
            {
                MessageBox.Show("성공적으로 추가되었습니다.");
                UpdateFriendsList_Click(this, new EventArgs());
                friendIDbox.Clear();
            }
            else
            {
                MessageBox.Show("알 수 없는 오류입니다.");
                friendIDbox.Clear();
            }
        }

        private async void StartChatting_Click(object sender, EventArgs e)
        {//dataformat : [&user1&user2&]
            if (friendsListBox.CheckedItems.Count < 1)
            {
                MessageBox.Show("혼자선 채팅할 수 없습니다.");
                return;
            }
            for (int i = 0; i < friendsListBox.CheckedItems.Count; i++) {
                foreach(string id in friendsListBox.UserState.Keys)
                {
                    if (id.Equals(friendsListBox.CheckedItems[i].ToString()) && !friendsListBox.UserState[id]) {
                        MessageBox.Show("로그아웃된 친구가 포함되어있습니다.");
                        return;
                    }
                }
            }
            if (DupleChecker.isOpen)
            {
                chatForm.Close();
                chatForm = null;
                DupleChecker.isOpen = false;
            }
            count = 0;
            max = 0;
            selectedUserArray.Clear();
            await Task.Run(() =>
            {
                bool leastOne = false;
                selectedUser = null;
                selectedUser = "[&"+this.id; //내아이디
                string id = null;
                for (int i = 0; i < friendsListBox.Items.Count; i++)
                {
                    id = friendsListBox.Items[i].ToString();
                    if (friendsListBox.GetItemChecked(friendsListBox.Items.IndexOf(id)))
                    {
                        leastOne = true;
                        selectedUser += "&"+id; //선택한 아이디
                        max++;
                    }
                }
                if (!leastOne)
                    selectedUser += "&]";
                else
                    selectedUser += "&]";
                string[] seluser = selectedUser.Split('&');
                for(int i = 1; i < seluser.Length - 1; i++)
                {
                    selectedUserArray.Add(seluser[i]);
                }
                byte[] MyId = Encoding.Default.GetBytes(selectedUser);
                N_stream.Write(MyId, 0, MyId.Length);
            });
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

        private void Open_calender_button_Click(object sender, EventArgs e)
        {
            cal = new Calender(this.id);
            cal.Show();
        }

        private void button1_Click(object sender, EventArgs e)//D-day설정 버튼(progress바 추가시 button2도 추가)
        {
            string startTime = null;
            string endTime = Dday_setEnd.Value.ToString("yyyy-MM-dd");
            string nowTime = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime T1 = DateTime.Parse(endTime);//종료시간
            DateTime T2 = DateTime.Parse(nowTime);//현재시간
            if (DateTime.Compare(T1, T2) < 0) {
                MessageBox.Show("잘못된 시간설정입니다.");
                return;
            }
            else if (subject_box.Text.Equals(""))
            {
                MessageBox.Show("주제를 입력하십시오.");
                return;
            }
            
            startTime=progressControler.setProgress1(id, nowTime, endTime, subject_box.Text);
            MessageBox.Show("D-day 추가 및 편집이 완료되었습니다.");
            subject.Text = subject_box.Text;
            start_time.Text = startTime;
            end_time.Text = endTime;
            //quary.connection.Open();
            Calcutate_Date1(startTime, nowTime, endTime);
        }
        private void get_schedule()
        {
            string nowTime = System.DateTime.Now.ToString("yyyy-MM-dd");
            List<string> scheduleBox = null;
                for (int i = 1; i <= 2; i++)
                {
                    scheduleBox = null;
                    scheduleBox = progressControler.getScheduleLine(id, i);
                   
                        if (i == 1 && scheduleBox!=null)
                        {
                            start_time.Text = scheduleBox[0];
                            end_time.Text = scheduleBox[1];
                            subject.Text = scheduleBox[2];
                            Calcutate_Date1(scheduleBox[0], nowTime, scheduleBox[1]);
                        }
                        else if(i==2 && scheduleBox!=null)
                        {
                            start_time2.Text = scheduleBox[0];
                            end_time2.Text = scheduleBox[1];
                            subject2.Text = scheduleBox[2];
                            Calcutate_Date2(scheduleBox[0], nowTime, scheduleBox[1]);//버튼2에선 2로
                        }
                }
         }
        private void Calcutate_Date1(string startTime, string nowTime, string endTime) {
            DateTime T1 = DateTime.Parse(endTime);
            DateTime T2 = DateTime.Parse(startTime);
            DateTime T3 = DateTime.Parse(nowTime);
            int flag = DateTime.Compare(T1, T3);
            if (flag == 0) {
                //MessageBox.Show("D-day 입니다!");
                try
                {
                    progressControler.DeleteProg1(id);
                    MessageBox.Show("D-day 입니다!");
                }
                catch (Exception err) { }
                finally
                {
                    start_time.Text = "label1";
                    end_time.Text = "label2";
                    subject.Text = "label3";
                    Dday_label.Text = "label4";
                    progress1.Value = 0;
                }
                return;
            }
            TimeSpan TS = T1 - T2; //end-start - start-time
            TimeSpan TG = T3 - T2; //now-start - start-time
            int totalTime = TS.Days;
            int thisTime = TG.Days;
            Dday_label.Text = "D-" + (totalTime - thisTime);
            if (Dday_label.Text.Length > 6) {
                progressControler.DeleteProg1(id);
                MessageBox.Show("D-Day 가용 시간이 초과하였습니다.");

                start_time.Text = "label1";
                end_time.Text = "label2";
                subject.Text = "label3";
                Dday_label.Text = "label4";
                progress1.Value = 0;
                return;
            }
            progress1.Minimum = 0;
            progress1.Maximum = totalTime;
            progress1.Step = 1;
            progress1.Value = thisTime;
        }
        private void Calcutate_Date2(string startTime, string nowTime, string endTime)
        {
            DateTime T1 = DateTime.Parse(endTime);
            DateTime T2 = DateTime.Parse(startTime);
            DateTime T3 = DateTime.Parse(nowTime);
            int flag = DateTime.Compare(T1, T3);
            if (flag == 0)
            {
                try
                {
                    progressControler.DeleteProg2(id);
                    MessageBox.Show("D-day 입니다!");
                }
                catch (Exception err) { }
                finally
                {
                    start_time2.Text = "label1";
                    end_time2.Text = "label2";
                    subject2.Text = "label3";
                    Dday_label2.Text = "label4";
                    progress2.Value = 0;
                   
                }
                return;
            }
            TimeSpan TS = T1 - T2; //end-start
            TimeSpan TG = T3 - T2; //now-start
            int totalTime = TS.Days;
            int thisTime = TG.Days;
            Dday_label2.Text = "D-" + (totalTime - thisTime);
            if (Dday_label2.Text.Length > 6)
            {
                progressControler.DeleteProg2(id);
                MessageBox.Show("D-Day 가용 시간이 초과하였습니다.");

                start_time2.Text = "label1";
                end_time2.Text = "label2";
                subject2.Text = "label3";
                Dday_label2.Text = "label4";
                progress2.Value = 0;
               
                return;
            }
            progress2.Minimum = 0; //progress2로
            progress2.Maximum = totalTime;
            progress2.Step = 1;
            progress2.Value = thisTime;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string startTime = null;
            string endTime = Dday_setEnd2.Value.ToString("yyyy-MM-dd");
            string nowTime = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime T1 = DateTime.Parse(endTime);//종료시간
            DateTime T2 = DateTime.Parse(nowTime);//현재시간
            if (DateTime.Compare(T1, T2) < 0)
            {
                MessageBox.Show("잘못된 시간설정입니다.");
                return;
            }
            else if (subject_box2.Text.Equals(""))
            {
                MessageBox.Show("주제를 입력하십시오.");
                return;
            }
            startTime=progressControler.setProgress2(id, nowTime, endTime, subject_box2.Text);
            MessageBox.Show("D-day 추가 및 편집이 완료되었습니다.");
            subject2.Text = subject_box2.Text;
            start_time2.Text = startTime;
            end_time2.Text = endTime;
            //quary.connection.Open();
            Calcutate_Date2(startTime, nowTime, endTime);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            if (DupleChecker.isOpen)
            {
                MessageBox.Show("아직 채팅중입니다.");
                return;
            }
            if (MessageBox.Show("로그아웃 하시겠습니까?", "채팅알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LogoutButton_Click(this, new EventArgs());
            }
            else
                return;
        }
    }
    public partial class FriendsListBox : CheckedListBox {
        List<Color> StatusColor = new List<Color>();
        public Dictionary<string, bool> UserState = new Dictionary<string, bool>();
        Quary quary = new Quary();
        friendsListControl friendsListControler = new friendsListControl();
        public string id {
            get; set;
        }
        public FriendsListBox(string id) {
            this.id = id;
        }
        public void Change_ShowState() {
            try
            {
                this.Invoke((MethodInvoker)delegate () {
                    int i = 0;
                    StatusColor.Clear();
                    UserState.Clear();
                    this.Items.Clear();
                    quary.connection.Open();
                    quary.reader = friendsListControler.UpdateFriendsList(id);
                    while (quary.reader.Read())
                    {
                        if (quary.reader["status"].ToString().Equals("red"))
                        {
                            this.Items.Add(quary.reader["ID"].ToString());
                            StatusColor.Add(Color.FromArgb(255, 0, 0));
                            UserState.Add(quary.reader["ID"].ToString(), false);
                        }
                        else
                        {
                            this.Items.Add(quary.reader["ID"].ToString());
                            StatusColor.Add(Color.FromArgb(0, 255, 0));
                            UserState.Add(quary.reader["ID"].ToString(), true);
                        }
                        i++;
                    }
                });
                
            }
            catch (Exception err) { }
            finally
            {
                quary.connection.Close();
                quary.reader.Close();
                friendsListControler.quary.reader.Close();
                friendsListControler.quary.connection.Close();
                user_friends.quary.reader.Close();
                user_friends.quary.connection.Close();
            }
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (StatusColor.Count <= e.Index)
            {
                base.OnDrawItem(e);
                return;
            }
            DrawItemEventArgs E = new DrawItemEventArgs(e.Graphics, e.Font, new Rectangle(e.Bounds.Location, e.Bounds.Size),
                e.Index, e.State, StatusColor[e.Index], Color.White);
            base.OnDrawItem(E);
        }
    }
}
