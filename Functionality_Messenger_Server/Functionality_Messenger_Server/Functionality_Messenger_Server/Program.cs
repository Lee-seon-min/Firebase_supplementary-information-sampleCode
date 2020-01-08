using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Functionality_Messenger_Server
{
    static class Program
    {
        public delegate void Running();
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Action RunChatServer=new Action(ChatServerRunner);
            Action RunHomeServer = new Action(HomeServerRunner);
            Task Runner1 = new Task(RunChatServer);
            Task Runner2 = new Task(RunHomeServer);

            Runner1.Start();
            Runner2.Start();

            Task.WaitAll(new Task[] { Runner1,Runner2});
        }
        private static void ChatServerRunner()
        {
            Application.Run(new ChatServer());
        }
        private static void HomeServerRunner()
        {
            Application.Run(new HomeServer());
        }
    }
}
