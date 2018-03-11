using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class ClientManager
    {
        private readonly IList<Client> _clients = new List<Client>();

        public void AddClient(TcpClient tcp)
        {
            Client client = new Client(tcp);
            lock (_clients)
            {
                _clients.Add(client);
            }
            Task.Factory.StartNew(() =>
            {
                try
                {
                    ManageClient(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore: {0}", ex.Message);
                    RemoveClient(client);
                }
            });
        }

        public void RemoveClient(Client c)
        {
            SendMessageToOthers(c, "Disconnesso");
            lock (_clients)
            {
                _clients.Remove(c);
            }
            c.Close();
        }

        private void ManageClient(Client c)
        {
            c.NickName = c.ReadLine();
            SendMessageToOthers(c, "Connesso");
            while (true)
            {
                string msg = c.ReadLine();
                if ("quit".Equals(msg))
                {
                    RemoveClient(c);
                    break;
                }
                else
                {
                    SendMessageToOthers(c, msg);
                }
            }
        }

        private void SendMessageToOthers(Client client, string msg)
        {
            Console.WriteLine(client.ToString() + ": " + msg);
            lock (_clients)
            {
                foreach (Client c in _clients)
                {
                    if (c != client)
                    {
                        c.WriteLine(client.NickName + ": " + msg);
                    }
                }
            }
        }

        public void Stop()
        {
            lock (_clients)
            {
                foreach (Client c in _clients)
                    c.Close();
                _clients.Clear();
            }
        }
    }
}
