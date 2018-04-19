using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UdpClient _client;

        public MainWindow()
        {
            InitializeComponent();
            InitClient();
        }

        private void InitClient()
        {
            IPEndPoint epLocal = new IPEndPoint(IPAddress.Any, 4000);
            _client = new UdpClient(epLocal);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            string ip = txtAddress.Text;
            int port = Convert.ToInt32(txtPort.Text);
            _client.Connect(ip, port);
            txtMessage.IsEnabled = true;
            btnSend.IsEnabled = true;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string msg = txtMessage.Text;
            byte[] dati = Encoding.ASCII.GetBytes(msg);
            _client.Send(dati, dati.Length);
        }
    }
}
