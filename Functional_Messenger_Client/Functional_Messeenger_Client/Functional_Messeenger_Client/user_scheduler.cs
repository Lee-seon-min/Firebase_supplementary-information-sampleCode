using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Functional_Messeenger_Client
{
    class user_scheduler
    {
        static public Quary quary = new Quary();
        static public MySqlDataReader getTime_by_date(string id,string date)
        {
            quary.command.CommandText = "select Time from " + id + "_scheduler" + " where Time='" + date + "'";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public void updateText(string id, string text, string date) {
            quary.command.CommandText = "update " + id + "_scheduler set Schedule='" + text + "' where Time='" + date + "'";
            quary.command.ExecuteNonQuery();
        }
        static public void insertText(string id, string text, string date)
        {
            quary.command.CommandText = "insert into " + id + "_scheduler values ('" + date + "' ,'" + text + "')";
            quary.command.ExecuteNonQuery();
        }
        static public MySqlDataReader getText_by_date(string id, string date)
        {
            quary.command.CommandText = "select Schedule from " + id + "_scheduler" + " where Time='" + date + "'";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
    }
}
