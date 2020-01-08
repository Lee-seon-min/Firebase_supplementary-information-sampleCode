using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Functional_Messeenger_Client
{
    class user_friends
    {
        static public Quary quary = new Quary();
        static public MySqlDataReader getUserFriends_ID(string id) {
            quary.connection.Open();
            quary.command.CommandText = "select ID from " + id + "_friends";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public void insert_friend(string uid,string fid,string state)
        {
            quary.connection.Open();
            quary.command.CommandText = "insert into " + uid + "_friends values ('" + fid + "', '" + state + "')";
            quary.command.ExecuteNonQuery();
        }
        static public MySqlDataReader getUserFriends_ID_Stat(string id)
        {
            quary.connection.Open();
            quary.command.CommandText = "select ID,status from " + id + "_friends";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
    }
}
