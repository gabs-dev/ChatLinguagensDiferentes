using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLinguagensDiferentes {
    public class SocketService {

        public Socket ServerSocket { get; set; }

        public Socket ListenerSocket { get; set; }

        public int ServerPort { get; set; }

        public int ClientPort { get; set; }

        public string ServerAddress { get; set; }

        public SocketService() {
            ServerPort = 6969;
            ClientPort = 6970;
            InitializeServerSocket();
            InitializeListenerSocket();
        }

        public void ConnectToServer(string serverAddress) {
            if (ServerSocket != null && ServerSocket.Connected)
                return;

            if (ServerSocket == null)
                throw new Exception("Nenhuma instância do Socket do servidor criada.");

            ServerSocket.Connect(serverAddress, ServerPort);

            this.ServerAddress = serverAddress;
        }

        public void SendMessage(string message) {
            if (!ServerSocket.Connected)
                throw new Exception("O servidor não está conectado.");

            ServerSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        private void InitializeServerSocket() {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void CloseAllConnections() {
            if (ServerSocket != null)
                ServerSocket.Close();

            if (ListenerSocket != null)
                ListenerSocket.Close();
        }

        private void InitializeListenerSocket() {
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var localIPAddress = GetLocalIPAddress();

            // Define a porta local e o endereco IP
            ListenerSocket.Bind(new IPEndPoint(localIPAddress, 6970));

            // Comeca a ouvir por conexoes de clientes. O valor 10 é o número máximo de conexões pendentes.
            ListenerSocket.Listen(10);
        }

        private IPAddress GetLocalIPAddress() {

            // Obtém a lista de todas as interfaces de rede da maquina.
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces().ToList();

            // Filtra as interfaces de rede que têm o tipo "Wireless80211"
            var networkInterface = networkInterfaces.Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && x.Name == "Wi-Fi").FirstOrDefault();

            if (networkInterface == null)
                throw new Exception("Erro ao recuperar o endereço IP da márquina.");


            // Recupera o endereço IPv4 da rede Wi-Fi
            var ipv4 = networkInterface.GetIPProperties().UnicastAddresses.Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();

            if (ipv4 == null)
                throw new Exception("Endereço IPv4 não encontrado");

            return ipv4.Address;
        }
    }
}
