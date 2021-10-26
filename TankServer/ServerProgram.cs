using System;
using System.Threading.Tasks;

namespace TankServer
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.StartServer();
            Console.WriteLine("Server started");
            Task.Factory.StartNew(() => server.Connects());
            Task.Factory.StartNew(() => server.SendInfo());
            while (true)
            {
               
            }
        }
    }
}
