using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Functional_Messeenger_Client
{
    class user_progress
    {
        static public Quary quary = new Quary();
        static public MySqlDataReader getPnum1(string id)
        {
            quary.command.CommandText = "select pnum from " + id + "_progress where pnum=1"; //button2에선 2로
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public MySqlDataReader getPnum2(string id)
        {
            quary.command.CommandText = "select pnum from " + id + "_progress where pnum=2"; //button2에선 2로
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public void updateProgress1(string id,string eTime, string subj)
        {
            quary.command.CommandText = "update " + id + "_progress set " +
                       "end='" + eTime + "', " + "subject='" + subj + "' where pnum=1";
            quary.command.ExecuteNonQuery();
        }
        static public void insertProgress1(string id, string sTime, string eTime, string subj)
        {
            quary.command.CommandText = "insert into " + id + "_progress values ('" + sTime + "', '" + eTime + "', '" + subj + "',1)";
            quary.command.ExecuteNonQuery();
        }
        static public MySqlDataReader getStartTimeProg1(string id)
        {
            quary.command.CommandText = "select start from " + id + "_progress where pnum=1";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public void updateProgress2(string id, string eTime, string subj)
        {
            quary.command.CommandText = "update " + id + "_progress set " +
                       "end='" + eTime + "', " + "subject='" + subj + "' where pnum=2";
            quary.command.ExecuteNonQuery();
        }
        static public void insertProgress2(string id, string sTime, string eTime, string subj)
        {
            quary.command.CommandText = "insert into " + id + "_progress values ('" + sTime + "', '" + eTime + "', '" + subj + "',2)";
            quary.command.ExecuteNonQuery();
        }
        static public MySqlDataReader getStartTimeProg2(string id)
        {
            quary.command.CommandText = "select start from " + id + "_progress where pnum=2";
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public MySqlDataReader getSchedule_all(string id,int i)
        {
            quary.command.CommandText = "select * from " + id + "_progress where pnum=" + i;
            quary.reader = quary.command.ExecuteReader();
            return quary.reader;
        }
        static public void delSchedule1(string id)
        {
            quary.command.CommandText = "delete from " + id + "_progress where pnum=1";
            quary.command.ExecuteNonQuery();
        }
        static public void delSchedule2(string id)
        {
            quary.command.CommandText = "delete from " + id + "_progress where pnum=2";
            quary.command.ExecuteNonQuery();
        }
    }
}
