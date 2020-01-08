using MySql.Data.MySqlClient;

namespace Functional_Messeenger_Client
{
    class Quary
    {
        static string URL = "server=192.168.35.191; database=messanger; user=mclient; password=mc123; port=3306";
        public MySqlConnection connection = new MySqlConnection();
        public MySqlCommand command = new MySqlCommand();
        public MySqlDataReader reader = null;
        public MySqlDataReader other_reader = null;
        
        public Quary()
        {
            connection.ConnectionString = URL;
            command.Connection = connection;
        }
    }
}
