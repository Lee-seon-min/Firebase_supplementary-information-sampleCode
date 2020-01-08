using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional_Messeenger_Client
{
    class shareControl
    {
        Quary quary = new Quary();
        public int ReceiveShareSchedule(string id, List<string> list)
        {
            try
            {
                user_scheduler.quary.connection.Open();
                quary.connection.Open();
                quary.reader = user_scheduler.getTime_by_date(id, list[0]);

                if (quary.reader.Read()) //해당 날짜가 존재하다면,
                {
                    quary.reader.Close();
                    user_scheduler.updateText(id, list[1], list[0]);
                    return 0;
                }
                else
                {
                    quary.reader.Close();
                    user_scheduler.insertText(id, list[1], list[0]);
                    return 1;
                }
            }
            catch (Exception err) { }
            finally
            {
                user_scheduler.quary.reader.Close();
                user_scheduler.quary.connection.Close();
                //quary.reader.Close();
                quary.connection.Close();
            }
            return -1;
        }
        public string request_getText(string id, string date)
        {
            try
            {
                user_scheduler.quary.connection.Open();
                quary.connection.Open();
                quary.reader = user_scheduler.getText_by_date(id, date);
                if (quary.reader.Read())
                    return quary.reader["Schedule"].ToString();
                return "";
            }
            finally
            {
                user_scheduler.quary.reader.Close();
                user_scheduler.quary.connection.Close();
                quary.reader.Close();
                quary.connection.Close();
            }
        }
    }
}
