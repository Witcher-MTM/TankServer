using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public static class Login
    {
        public static bool IsLogin { get; set; }
        public static string UserLogin { get; set; }
        public static DateTime UserDataRegistr { get; set; }

        public static void StartLogin()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}
