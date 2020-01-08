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
namespace Functionality_Messenger_Server
{
    class ChatClientAction
    {
        public delegate void MessageReceive_Throw(string Orogin_message, string S_message, string name, string users);
        public event MessageReceive_Throw Receive_Action;
        public delegate void ShutdownCode_Throw(TcpClient client, string name,string usere);
        public event ShutdownCode_Throw Shutdown_Action;

        NetworkStream stream = null;
       
        public TcpClient client{ //연결된 해당 클라이언트의 객체
            get; set;
        }
        public string name //해당 클라이언트의 id
        {
            get;set;
        }
        public string users
        {
            get;set;
        }
        public ChatClientAction(TcpClient Client, string name)
        {
            client = Client; 
            this.name = name;
        }
        public void Start_Thread_of_Client() //아래 함수 실행
        {
            Thread Runner = new Thread(Checking_and_EventAction);
            Runner.IsBackground = true;
            Runner.Start();
        }
        private void Checking_and_EventAction()
        {
            byte[] Stream_data = new byte[1024];
            string S_message = null;
            string R_message = null;
            string message = null;
            List<string> To = new List<string>();
            int count;
            int Len;
            try
            {
                while (true)
                {
                    users = null;
                    To.Clear();
                    stream = client.GetStream(); //각 클라이언트와의 스트림
                    Len = stream.Read(Stream_data, 0, Stream_data.Length);
                    message = Encoding.Default.GetString(Stream_data, 0, Len);
                    S_message = "Receiving..." + message; //메세지를 받았다면

                    if (message.StartsWith("~(") && message.EndsWith(")~")) //~(안녕[@user1@user2@])~
                    {
                        R_message = (message.Split('(')[1]).Split('[')[0];
                        count = message.Split('@').Length;
                        string[] tempTarget = message.Split('@');
                        for (int i = 1; i < count - 1; i++)
                            To.Add(tempTarget[i]);
                        users = To[0];
                        for (int i = 1; i < To.Count; i++)
                            users += "," + To[i];
                    }
                    else if (message.StartsWith("[$") && message.EndsWith("@")) //[$시간$내용$]@user1@user2@
                    {
                        R_message = message.Split('@')[0];
                        count = message.Split('@').Length;
                        string[] tempTarget = message.Split('@');
                        for (int i = 1; i < count - 1; i++)
                            if (!tempTarget[i].Equals(name))
                                To.Add(tempTarget[i]);
                        if (To.Count == 0)
                        {
                            users = ",";
                        }
                        else
                        {
                            users = To[0];
                            for (int i = 1; i < To.Count; i++)
                                users += "," + To[i];
                        }
                    }
                    else if (message.StartsWith(Packet.Shutdown) && message.EndsWith("@")) // ///shutdown///@user1@user2@
                    {
                        R_message = message.Split('@')[0]; //셧다운 패킷
                        count = message.Split('@').Length;
                        string[] tempTarget = message.Split('@');
                        for (int i = 1; i < count - 1; i++)
                            if (!tempTarget[i].Equals(name))
                                To.Add(tempTarget[i]);
                        if (To.Count == 0)
                        {
                            users = ",";
                        }
                        else
                        {
                            users = To[0];
                            for (int i = 1; i < To.Count; i++)
                                users += "," + To[i];
                        }
                    }
                    else if (message.StartsWith("<@") && message.EndsWith("@>")) //<@user1,user2@>
                    {
                        R_message = message;
                        string[] temp = (message.Split('@')[1]).Split(',');
                        count = temp.Length;
                        for (int i = 0; i < count; i++)
                            if (!temp[i].Equals(name))
                                To.Add(temp[i]);
                        if (To.Count == 0)
                        {
                            users = ",";
                        }
                        else
                        {
                            users = To[0];
                            for (int i = 1; i < To.Count; i++)
                                users += "," + To[i];
                        }
                    }
                    Receive_Action(R_message, S_message, name,users); //receiveMessage함수 실행
                    if (R_message.Equals(Packet.Shutdown))
                    {
                        Shutdown_Action(client, name,users); //ExitServer함수 실행
                        return; //해당 클라이언트의 종료
                    }
                    
                }
            }
            //finally로 스트림을 받아야할까?
            catch (SocketException se)
            {
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
