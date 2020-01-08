using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional_Messeenger_Client
{
    class progressControl
    {
        public Quary quary = new Quary();
        public string setProgress1(string id,string sTime,string eTime,string subj)
        {
            try
            {
                quary.connection.Open();
                user_progress.quary.connection.Open();
                quary.reader = user_progress.getPnum1(id);
                if (quary.reader.Read())
                { //해당 번호의 progress가 존재하면
                    quary.reader.Close();
                    user_progress.updateProgress1(id, eTime, subj);
                    quary.reader = user_progress.getStartTimeProg1(id);
                    quary.reader.Read();
                    return quary.reader["start"].ToString();
                }
                else
                {
                    quary.reader.Close();
                    user_progress.insertProgress1(id, sTime, eTime, subj);
                    quary.reader = user_progress.getStartTimeProg1(id);
                    quary.reader.Read();
                    return quary.reader["start"].ToString();
                }
            }
            catch (Exception err) { }
            finally
            {
                if (user_progress.quary.reader != null)
                    user_progress.quary.reader.Close();
                user_progress.quary.connection.Close();
                quary.reader.Close();
                quary.connection.Close();
            }
            return null;
        }
        public string setProgress2(string id, string sTime, string eTime, string subj)
        {
            try
            {
                quary.connection.Open();
                user_progress.quary.connection.Open();
                quary.reader = user_progress.getPnum2(id);
                if (quary.reader.Read())
                { //해당 번호의 progress가 존재하면
                    quary.reader.Close();
                    user_progress.updateProgress2(id, eTime, subj);
                    quary.reader = user_progress.getStartTimeProg2(id);
                    quary.reader.Read();
                    return quary.reader["start"].ToString();
                }
                else
                {
                    quary.reader.Close();
                    user_progress.insertProgress2(id, sTime, eTime, subj);
                    quary.reader = user_progress.getStartTimeProg2(id);
                    quary.reader.Read();
                    return quary.reader["start"].ToString();
                }
            }
            catch (Exception err) { }
            finally
            {
                if (user_progress.quary.reader != null)
                    user_progress.quary.reader.Close();
                user_progress.quary.connection.Close();
                quary.reader.Close();
                quary.connection.Close();
            }
            return null;
        }
        public List<string> getScheduleLine(string id,int i)
        {
            try
            {
                List<string> list = null;
                quary.connection.Open();
                user_progress.quary.connection.Open();
                quary.reader = user_progress.getSchedule_all(id, i);
                if (quary.reader.Read())
                {
                    list = new List<string>();
                    list.Add(quary.reader["start"].ToString());
                    list.Add(quary.reader["end"].ToString());
                    list.Add(quary.reader["subject"].ToString());
                }
                return list;
            }
            finally
            {
                if(quary.reader!=null)
                    quary.reader.Close();
                quary.connection.Close();
                user_progress.quary.reader.Close();
                user_progress.quary.connection.Close();
            }
        }
        public void DeleteProg1(string id) {
            try
            {
                user_progress.quary.connection.Open();
                user_progress.delSchedule1(id);
            }
            finally
            {
                user_progress.quary.connection.Close();
            }
        }
        public void DeleteProg2(string id)
        {
            try
            {
                user_progress.quary.connection.Open();
                user_progress.delSchedule2(id);
            }
            finally
            {
                user_progress.quary.connection.Close();
            }
        }
    }
}
