using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GSessionManager
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Manager man = new Manager();
            bool init = man.Start();
            if (init)
            {
                IntPtr dummy = man.Handle;
                Application.Run();
            }
        }
    }
}
