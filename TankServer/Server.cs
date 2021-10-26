using Client_Graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TankDLL;

namespace TankServer
{
    public class Server
    {
        public int Client_ID;
        private string ipAddr;
        private int port;
        private IPEndPoint ipPoint;
        public Socket socket;
        public Socket socketclient;
        public List<Client> clients;
        public List<Task> tasks;
        public Server()
        {
            this.Client_ID = -1;
            this.ipAddr = "127.0.0.1";
            this.port = 8000;
            this.ipPoint = new IPEndPoint(IPAddress.Parse(ipAddr), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.clients = new List<Client>();
            this.tasks = new List<Task>();

        }
        public void StartServer()
        {
            try
            {

                this.socket.Bind(ipPoint);
                this.socket.Listen(10);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


        }
        public void Connects()
        {
            while (true)
            {

                this.socketclient = this.socket.Accept();


                clients.Add(new Client(socketclient));
                this.Client_ID++;
                clients[clients.Count - 1].ID = this.Client_ID;
                tasks.Add(new Task(() =>
                {
                    Task.Factory.StartNew(() => GetInfo());
                    
                }));
                Console.WriteLine("New player has been conected");
                tasks.Last().Start();
                Console.WriteLine("New task for new player started");
            }

        }
        public StringBuilder GetInfo()
        {
           
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            byte[] data = new byte[1024];
            for (int i = 0; i < clients.Count; i++)
            {
                do
                {
                    try
                    {
                        bytes = clients[i].socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    catch (Exception)
                    {


                    }

                } while (clients[i].socket.Available > 0);
            }
            if (builder.ToString() != "")
            {
                Console.WriteLine(builder.ToString());
            }
            return builder;
        }
    }
}
