using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Functional_Messeenger_Client
{
    class friendsListControl
    {
        public Quary quary = new Quary();
       
        public MySqlDataReader UpdateFriendsList(string id)
        {
            quary.connection.Open();
            quary.reader = user_friends.getUserFriends_ID_Stat(id);
            return quary.reader;
        }
    }
}
