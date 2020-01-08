using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional_Messeenger_Client
{
    class addFriendControl
    {
        public Quary quary = new Quary();
        public int Checkfriend(string fid,string uid)
        {
            bool flag = false;
            string State = null;
            try
            {
                quary.connection.Open();
                quary.reader = userlist.getUserList_ID_Stat();
                while (quary.reader.Read())
                {
                    if (quary.reader["ID"].ToString().Equals(fid))
                    {
                        if (quary.reader["ID"].ToString().Equals(uid))
                        {
                            //MessageBox.Show("자기자신을 추가할수는 없습니다.");
                            //friendIDbox.Clear();
                            quary.reader.Close();
                            quary.connection.Close();
                            return -1;
                        }
                        flag = true;
                        State = quary.reader["status"].ToString();
                        quary.other_reader = user_friends.getUserFriends_ID(uid);
                        while(quary.other_reader.Read())
                        {
                            if (quary.other_reader["ID"].ToString() == fid)
                            {
                                //MessageBox.Show("이미 등록한 친구입니다.");
                                //friendIDbox.Clear();
                                quary.other_reader.Close();
                                quary.reader.Close();
                                quary.connection.Close();
                                return 0;
                            }
                        }
                    }
                }
                user_friends.quary.connection.Close();
                quary.reader.Close();
                if (!flag)
                {
                    //MessageBox.Show("존재하지 않는 ID입니다.");
                    //friendIDbox.Clear();
                    quary.reader.Close();
                    quary.connection.Close();
                    return 1;
                }
                else
                {
                    user_friends.insert_friend(uid, fid, State);
                    return 2;
                    //UpdateFriendsList_Click(this, new EventArgs());
                    //MessageBox.Show("성공적으로 추가되었습니다.");
                }
            }
            catch (Exception err) { }
            finally
            {
                if(quary.reader!=null)
                    quary.reader.Close();
                if (quary.other_reader != null)
                    quary.other_reader.Close();
                if (quary.connection!=null)
                    quary.connection.Close();
                if(userlist.quary.reader!=null)
                    userlist.quary.reader.Close();
                if(userlist.quary.connection!=null)
                    userlist.quary.connection.Close();
                if(user_friends.quary.reader!=null)
                    user_friends.quary.reader.Close();
                if(user_friends.quary.connection!=null)
                user_friends.quary.connection.Close();
            }
            return 3;
        }
    }
}
