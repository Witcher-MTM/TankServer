using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class RegisterForm : Form
    {
        public User user = new User();
        public GameMail mail = new GameMail();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                user.Login = this.textBox1.Text;
                user.Password = this.textBox2.Text;
                user.Email = this.textBox3.Text;
                if (File.Exists($"{user.Login}.json"))
                {
                    MessageBox.Show("Такой логин уже существует!");
                }
                else
                {
                    SendPassword();
                    user.Password = ComputeSha256Hash(user.Password);
                    string json = JsonSerializer.Serialize<User>(user);
                    //if (File.Exists($"{user.Login}.json"))
                    //{
                    //    //user = JsonSerializer.Deserialize<User>(json);
                    //    //if (user.Email == this.textBox3.Text)
                    //    //{
                    //    //    MessageBox.Show("Такая почта уже существует!");
                    //    //}
                    //}
                    File.WriteAllText($"{user.Login}.json", json);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
            
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
        private void SendPassword()
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(user.Email, "RubickTanks");
            // кому отправляем
            MailAddress to = new MailAddress(user.Email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = $"Hello {user.Login}";
            // текст письма
            m.Body = ComputeSha256Hash(user.Password);
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(mail.Email, mail.Pass);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
