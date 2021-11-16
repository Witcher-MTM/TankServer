using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit.Net.Smtp;


namespace WinFormsApp1
{
    public partial class RegisterForm : Form
    {
        public User user = new User();
        public GameMail admin = new GameMail();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                user.Login = this.textBox1.Text;
                if (File.Exists($"{user.Login}.json"))
                {
                    MessageBox.Show("Такой логин уже существует!");
                }
                else if (IsValidEmail(this.textBox3.Text))
                {
                    user.Email = this.textBox3.Text;
                    user.Password = this.textBox2.Text;
                    user.Password = ComputeSha256Hash(user.Password);
                    SendEmailAsync().GetAwaiter().GetResult();
                    user.Registr = DateTime.Today;
                    string json = JsonSerializer.Serialize<User>(user);
                    File.WriteAllText($"{user.Login}.json", json);
                   
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect email");
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
        //private void SendPassword()
        //{
        //    // отправитель - устанавливаем адрес и отображаемое в письме имя
        //    MailAddress from = new MailAddress(user.Email, "RubickTanks");
        //    // кому отправляем
        //    MailAddress to = new MailAddress(user.Email);
        //    // создаем объект сообщения
        //    MailMessage m = new MailMessage(from, to);
        //    // тема письма
        //    m.Subject = $"Hello {user.Login}";
        //    // текст письма
        //    m.Body = ComputeSha256Hash(user.Password);
        //    // письмо представляет код html
        //    m.IsBodyHtml = true;
        //    // адрес smtp-сервера и порт, с которого будем отправлять письмо
        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        //    // логин и пароль
        //    smtp.Credentials = new NetworkCredential(admin.Email, admin.Pass);
        //    smtp.EnableSsl = true;
        //    smtp.Send(m);
        //}

        public async Task SendEmailAsync()
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", admin.Email));
            emailMessage.To.Add(new MailboxAddress("", user.Email));
            emailMessage.Subject = "Rubick Tanks";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Your pass:"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(admin.Email, admin.Pass);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
