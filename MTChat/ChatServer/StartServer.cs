using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;

namespace ChatServer
{
    public class StartServer
    {
        const int Port = 2340;

        public static void Main(string[] args)
        {
            Console.WriteLine("Avvio Server (CTRL+C per uscire)");
            TcpListener listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            var cm = new ClientManager();
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    cm.AddClient(client);
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
    }
}
