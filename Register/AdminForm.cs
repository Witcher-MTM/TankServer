using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class AdminForm : Form
    {
        List<string> BodySms = new List<string>();
        List<string> HeaderSms = new List<string>();
        List<DateTimeOffset> dataSms = new List<DateTimeOffset>();
        ImapClient client = new ImapClient();
        IMailFolder inbox;
        public AdminForm()
        {
            InitializeComponent();
        }

        public List<string> DownloadMessages()
        {
            GameMail admin = new GameMail();

            client.Connect("imap.gmail.com", 993, true);
            client.Authenticate(admin.Email, admin.Pass);
            inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            for (int i = 0; i < inbox.Count; i++)
            {
                HeaderSms.Add($"[{i}] - {inbox.GetMessage(i).Subject}");
                BodySms.Add(inbox.GetMessage(i).TextBody);
            }
            return HeaderSms;

        }

        private void ListMms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListMms.SelectedIndex == -1)
            {
                ListMms.SelectedIndex = 0;
            }
            textBox1.Text = BodySms[ListMms.SelectedIndex];

        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            ListMms.Items.AddRange(DownloadMessages().ToArray());
        }
        bool clicked = true;
        private void FilterBtn_Click(object sender, EventArgs e)
        {
            if (clicked)
            {
                clicked = false;
                this.FilterByName.Visible = true;
                this.DataFilterBtn.Visible = true;
            }
            else if (clicked == false)
            {
                clicked = true;
                this.FilterByName.Visible = false;
                this.DataFilterBtn.Visible = false;
            }

        }
        bool dataFilter = false;
        private void DataFilterBtn_Click(object sender, EventArgs e)
        {
            this.ListMms.Items.Clear();
            this.BodySms.Clear();
            this.dataSms.Clear();
            if (dataFilter)
            {
                dataFilter = false;
            }
            else
            {
                dataFilter = true;
            }
            for (int i = 0; i < inbox.Count; i++)
            {
                    dataSms.Add(inbox.GetMessage(i).Date);
            }
            dataSms.Sort();
            if (dataFilter)
            {
                for (int j = 0; j < dataSms.Count; j++)
                {
                    ListMms.Items.Add(HeaderSms[j]);
                    BodySms.Add(inbox.GetMessage(j).TextBody);
                }
            }
            else
            {
                for (int j = dataSms.Count-1; j > 0; j--)
                {
                    ListMms.Items.Add(HeaderSms[j]);
                    BodySms.Add(inbox.GetMessage(j).TextBody);
                }
            }
            
        }
       
        private void FilterByName_Click(object sender, EventArgs e)
        {
            
            this.textBoxByName.Visible = true;
            this.textBoxByName.Focus();
            if (textBox1.Text.Length>0)
            {
               
                this.ListMms.Items.Clear();
                HeaderSms.Clear();
                BodySms.Clear();
                for (int i = 0; i < inbox.Count; i++)
                {
                    if (inbox.GetMessage(i).From.ToString().ToLower().Contains(textBoxByName.Text.ToLower()))
                    {
                        HeaderSms.Add($"[{i}] - {inbox.GetMessage(i).Subject}");
                        BodySms.Add(inbox.GetMessage(i).TextBody);
                    }
                }
                ListMms.Items.AddRange(HeaderSms.ToArray());
            }
        }
    }

}
