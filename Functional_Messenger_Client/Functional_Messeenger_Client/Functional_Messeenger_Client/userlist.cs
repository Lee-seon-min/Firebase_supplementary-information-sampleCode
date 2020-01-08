using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Functional_Messeenger_Client
{
    class userlist
    {
        static public Quary quary = new Quary();
        static public MySqlDataReader getUserList_ID_PW()
        {
            quary.connection.Open();
            quary.command.CommandText = "select ID,PW from userlist";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public MySqlDataReader getUserList_ID()
        {
            quary.connection.Open();
            quary.command.CommandText = "select ID from userlist";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public void insertUser(string id,string pw)
        {
            quary.connection.Open();
            quary.command.CommandText = "create table " + id + "_Scheduler (" +
                "Time TEXT NOT NULL," +
                "Schedule TEXT NOT NULL)";
            quary.command.ExecuteNonQuery();
            quary.command.CommandText = "create table " + id + "_Friends (" +
                "ID varchar(30) NOT NULL," +
                "status varchar(20)," +
                "PRIMARY KEY(ID))";
            quary.command.ExecuteNonQuery();
            quary.command.CommandText = "create table " + id + "_progress (" +
                "start varchar(20) NOT NULL," +
                "end varchar(20) NOT NULL," +
                "subject varchar(20)," +
                "pnum int(1) NOT NULL)";
            quary.command.ExecuteNonQuery();
            quary.command.CommandText = "insert into userlist values ('" + id + "', '" + pw + "', '" + "red')";
            quary.command.ExecuteNonQuery();
        }
        static public MySqlDataReader getUserList_ID_Stat()
        {
            quary.connection.Open();
            quary.command.CommandText = "select ID,status from userlist";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
    }
}
