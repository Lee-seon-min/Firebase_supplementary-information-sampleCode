using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional_Messeenger_Client
{
    class joinControl
    {
        public Quary quary = new Quary();
        public bool isDuple(string id) {
            try
            {
                quary.connection.Open();
                quary.reader = userlist.getUserList_ID();
                while (quary.reader.Read()) {
                    if (quary.reader["ID"].ToString().Equals(id))
                    {
                        return false;
                    }
                }
                return true;
            }
            finally
            {
                quary.reader.Close();
                quary.connection.Close();
                userlist.quary.reader.Close();
                userlist.quary.connection.Close();
            }
        }
        public void request_InsertUser(string id,string pw)
        {
            try
            {
                userlist.insertUser(id, pw);
            }
            finally
            {
                userlist.quary.connection.Close();
            }
        }
    }
}
