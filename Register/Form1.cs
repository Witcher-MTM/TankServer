using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public User user = new User();
        public string json = string.Empty;
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                user.Login = this.textBox1.Text;
                if (File.Exists($"{user.Login}.json"))
                {
                    user = JsonSerializer.Deserialize<User>(File.ReadAllText($"{user.Login}.json"));
                    if (user.Password == ComputeSha256Hash(this.textBox2.Text))
                    {
                        MessageBox.Show("Вы успешно вошли!");
                        Login.IsLogin = true;
                        Login.UserDataRegistr = user.Registr;
                        Login.UserLogin = user.Login;
                        this.Close();
                    }
                    else if(user.Password == this.textBox2.Text)
                    {
                        MessageBox.Show("Вы успешно вошли!");
                        Login.IsLogin = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует!\nСначала зарегестрируйтесь!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            this.Hide();
            register.ShowDialog();
            this.Show();
        }
        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminForm admin = new AdminForm();
            this.Hide();
            admin.ShowDialog();
            this.Show();
        }
    }
}
