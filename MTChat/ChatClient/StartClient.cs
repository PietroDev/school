using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatClient
{
    public class StartClient
    {
        const string Address = "localhost";
        const int Port = 2340;

        public static void Main(string[] args)
        {
            Console.WriteLine("Avvio Client");
            using (TcpClient client = new TcpClient())
            {
                client.Connect(Address, Port);
                Console.WriteLine("Connesso al server.");
                using (NetworkStream ns = client.GetStream())
                {
                    StreamReader reader = new StreamReader(ns);
                    StreamWriter writer = new StreamWriter(ns);
                    writer.AutoFlush = true;

                    Console.Write("Inserisci il tuo nickname: ");
                    string nick = Console.ReadLine();
                    writer.WriteLine(nick);

                    Console.WriteLine("Inizia la conversazione {0}:", nick);

                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            ReadFromNetwork(reader);
                        } catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        } finally {
                            Console.WriteLine("Connessione chiusa");
                            Environment.Exit(0);
                        }
                    });
                    WriteToNetwork(writer);
                    reader.Close();
                    writer.Close();
                }
            }
        }

        private static void ReadFromNetwork(StreamReader reader)
        {
            while (true)
            {
                string msg = reader.ReadLine();
                if (msg == null)
                    return;
                Console.WriteLine(msg);
            }
        }

        private static void WriteToNetwork(StreamWriter writer)
        {
            while (true)
            {
                string msg = Console.ReadLine();
                writer.WriteLine(msg);
                if ("quit".Equals(msg))
                    break;
            }
        }
    }
}
