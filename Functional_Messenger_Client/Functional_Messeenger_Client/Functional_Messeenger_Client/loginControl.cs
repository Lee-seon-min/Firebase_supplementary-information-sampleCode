using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional_Messeenger_Client
{
    class loginControl
    {
        public Quary quary = new Quary();
        public bool flag=false;
        public bool isCorrect(string id, string pw) {
            try
            {
                quary.connection.Open();
                quary.reader = userlist.getUserList_ID_PW();
                flag = false;
                while (quary.reader.Read())
                {
                    if (quary.reader["ID"].ToString() == id && quary.reader["PW"].ToString() == pw)
                        flag = true; //query에 의해 결정
                    if (flag)
                        break;
                }
                return flag;
            }
            finally
            {
                quary.reader.Close();
                quary.connection.Close();
                userlist.quary.reader.Close();
                userlist.quary.connection.Close();
            }
          
        }
    }
}
