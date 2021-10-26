﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TankDLL;

namespace Client_Graphic
{
    public class Client
    {
        public int ID;
        public string ipAddr;
        public int port;
        public IPEndPoint iPEndPoint;
        public Socket socket;
        private string Ip;
        public Client()
        {
            this.ID++;
            this.ipAddr = "127.0.0.1";
            this.port = 8000;
            this.iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAddr), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
        }
        public Client(Socket socket)
        {

            this.socket = socket;
           
        }
        public void Connect()
        {
            socket.Connect(iPEndPoint);
            this.Ip = socket.RemoteEndPoint.ToString();
        }
        public bool SendInfo(Tank tank)
        {

            bool check = false;
            try
            {
                string json = string.Empty;
                json = JsonSerializer.Serialize<Tank>(tank);
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
            catch (Exception ex) {
            
            }

            return stringBuilder;
        }
    }
}
