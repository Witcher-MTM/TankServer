using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public static class Login
    {
        public static bool IsLogin { get; set; }
        public static bool TryLogin { get; set; }
        public static bool TryRegistr { get; set; }

        public static void StartLogin()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
