using Client_Graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TankDLL;
using MapDLL;
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
        private List<Tank> tanks;
        private Map map;
        public Server()
        {
            this.Client_ID = -1;
            this.ipAddr = "127.0.0.1";
            this.port = 8000;
            this.ipPoint = new IPEndPoint(IPAddress.Parse(ipAddr), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.clients = new List<Client>();
            this.tasks = new List<Task>();
            this.tanks = new List<Tank>();
            this.map = new Map();

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
                Console.WriteLine("Start", ex.Message);
               
            }


        }
        public void Connects()
        {
            map.SaveMap();
            while (true)
            {
                this.socketclient = this.socket.Accept();
                clients.Add(new Client(socketclient));
                this.Client_ID++;
                clients[clients.Count - 1].ID = this.Client_ID;
                tanks.Add(new Tank());
                tanks.Last().TankID++;

                tasks.Add(new Task(() => GetInfo()));

                Console.WriteLine("New player has been conected");
                Console.WriteLine($"Clients - {clients.Count}\n{clients.Last().socket.RemoteEndPoint.ToString()}");
                tasks.Last().Start();
                Console.WriteLine("New task for new player started");
            }
        }
        public void GetInfo()
        {
            int user = 0;
            string json = String.Empty;
            user = clients.Count - 1;
           
            while (clients[user].socket.Connected)
            {
                try
                {
                    if (clients[user].socket.Connected)
                    {
                        json = GetInfo(user).ToString();
                        tanks[user] = JsonSerializer.Deserialize<Tank>(json);
                        tanks[user].TankID = user + 1;
                    }
                }
                catch (Exception)
                {
                }
            }
            Console.WriteLine($"User {clients[user].socket.RemoteEndPoint.ToString()} disconnect");
            clients.RemoveAt(user);
            tanks.RemoveAt(user);

            if (user > clients.Count)
            {
                user--;
            }
        }
      
        public StringBuilder GetInfo(int user)
        {
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            byte[] data = new byte[1024];
            if (clients.Count != 0)
            {
                do
                {
                    try
                    {
                        if (clients[user].socket.Connected)
                        {
                            bytes = clients[user].socket.Receive(data, data.Length, 0);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        
                    }
                    catch (Exception)
                    {
                    }

                } while (clients[user].socket.Available > 0);

                if (builder.ToString() != "")
                {
                    try
                    {
                        Console.WriteLine(builder.ToString());
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return builder;
        }
        public void SendInfo()
        {
            string json = string.Empty;
            while (true)
            {
                try
                {
                    
                    json = JsonSerializer.Serialize<List<Tank>>(tanks);
                    foreach (var item in clients)
                    {
                        if (item.socket.Connected)
                        {
                            item.socket.Send(Encoding.Unicode.GetBytes(json));
                        }
                       
                    }
                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[SEND]", ex.Message);
                }
            }
        }
    }
}
