using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional_Messeenger_Client
{
    class Packet
    {
        public static string Shutdown = "///SHUTDOWN///";
        public static string Approve = "///APPROVE///";
        public static string LoginSignal = "///LOGIN///";
        public static string LogoutSignal = "///LOGOUT///";
        public static string ChangeSignal = "///CHANGE///";
        public static string ChatResponse_ok = "///OKJOINCHAT///";
        public static string ChatResponse_no = "///NOJOINCHAT///";
    }
}
