using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class ClientForm
    {
        public int ID;
        public string ipAddr;
        public int port;
        public IPEndPoint iPEndPoint;
        public Socket socket;
        public ClientForm()
        {
           
        }
        public bool SendInfo(User user)
        {
            bool check = false;
            try
            {
                string json = string.Empty;
                json = JsonSerializer.Serialize<User>(user);
                socket.Send(Encoding.Unicode.GetBytes(json));
                check = true;
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }
        public StringBuilder GetInfo()
        {
            byte[] data = new byte[1024];
            int bytes = 0;
            string json = String.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                do
                {
                    bytes = socket.Receive(data);
                    stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));

                } while (socket.Available > 0);
            }
            catch (Exception)
            {

            }

            return stringBuilder;
        }
    }
}
