using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1
{
    public class User
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Registr { get; set; }
        public DateTime UserLastOnline { get; set; }
        public User()
        {
            Login = "";
            Password = "";
            Email = "";
        }
        public User(string login, string password, string email)
        {
            Login = login;
            Password = password;
            Email = email;
        }
    }
}
