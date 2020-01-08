using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionality_Messenger_Server
{
    class Packet
    {
        public static string Shutdown = "///SHUTDOWN///";
        public static string Approve = "///APPROVE///";
        public static string LoginSignal = "///LOGIN///";
        public static string LogoutSignal = "///LOGOUT///";
        public static string ChangeSignal = "///CHANGE///";
    }
}
