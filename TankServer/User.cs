using System;
using System.Collections.Generic;
using System.Text;

namespace TankServer
{
    public class User
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EntryPass { get; set; }
        public User()
        {
            Login = "";
            Password = "";
            Email = "";
            EntryPass = "";
        }
        public User(string login, string password, string email)
        {
            Login = login;
            Password = password;
            Email = email;
        }
    }
}
