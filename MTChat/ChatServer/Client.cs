using System;
using System.IO;
using System.Net.Sockets;

namespace ChatServer
{

    public class Client
    {

        private readonly TcpClient _client;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        public Client(TcpClient client)
        {
            _client = client;
            NetworkStream ns = client.GetStream();
            _reader = new StreamReader(ns);
            _writer = new StreamWriter(ns);
            _writer.AutoFlush = true;
        }

        public string NickName
        {
            get; set;
        }

        public string ReadLine()
        {
            return _reader.ReadLine();
        }

        public void WriteLine(string msg)
        {
            _writer.WriteLine(msg);
        }

        public void Close()
        {
            _reader.Close();
            _writer.Close();
            _client.Close();
        }

        public override string ToString()
        {
            return _client.Client.RemoteEndPoint.ToString() + " (" + NickName + ")";
        }
    }
}
