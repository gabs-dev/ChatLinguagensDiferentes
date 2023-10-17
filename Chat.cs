using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace ChatLinguagensDiferentes {
    public partial class Chat : Form 
    {
        private Socket socket;

        public Chat() {
            InitializeComponent();
            InitializeSocket();
        }

        private void Form1_Load(object sender, EventArgs e) {
            
        }

        private void ButtonConectar_Click(object sender, EventArgs e) {
            var enderecoIPServidor = TextBoxEnderecoServidor.Text.Trim();

            if (enderecoIPServidor.Count() == 0) {
                ShowMessageBox("Campo vazio!", "O endereço IP do servidor é obrigtório.", MessageBoxIcon.Error);
                return;
            }

            /*// Cria um socket TCP
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var enderecoIPLocal = IPAddress.Parse(RecuperarEnderecoIPLocal());

            // Define a porta local e o endereço IP
            socket.Bind(new IPEndPoint(enderecoIPLocal, 6969));*/

            // Conecta ao servidor
            socket.Connect(enderecoIPServidor, 6969);

            // Inicia uma thread para receber mensagens
            new Thread(() => {
                while (true) 
                {
                    // Recebendo uma mensagem
                    byte[] bytes = new byte[2048];
                    socket.Receive(bytes);

                    // Convertendo a mensagem em string
                    string mensagem = Encoding.UTF8.GetString(bytes).Trim();

                    // Exibe a mensagem
                    TextBoxHistorico.Text += $"[Pedro]: {mensagem}\n";
                }
            }).Start();
        }

        private void ButtonEnviarMensagem_Click(object sender, EventArgs e) {
            /*if (socket == null || !socket.Connected) {
                ShowMessageBox("Erro de conexão!", "Nenhuma conexão via socket foi inicializada. " +
                    "Primeiro estabeleça conexão com o outro participante.", MessageBoxIcon.Error);
                return;
            }*/

            string mensagem = TextBoxMensagem.Text;

            // Envia a mensagem
            //socket.Send(Encoding.UTF8.GetBytes(mensagem));

            // Adiciona a minha mensagem ao histórico e limpa e campo de mensagem
            TextBoxHistorico.Text += $"[Eu]: {mensagem}" + Environment.NewLine;
            TextBoxMensagem.Text = "";
            TextBoxMensagem.Focus();
        }

        private string RecuperarEnderecoIPLocal() {

            // Obtém a lista de todas as interfaces de rede da máquina.
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces().ToList();

            // Filtra as interfaces de rede que têm o tipo "Wireless80211"
            var networkInterface = networkInterfaces.Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && x.Name == "Wi-Fi").FirstOrDefault();

            if (networkInterface == null)
                throw new Exception("Erro ao recuperar o endereço IP da márquina.");


            // Recupera o endereço IPv4 da rede Wi-Fi
            var ipv4 = networkInterface.GetIPProperties().UnicastAddresses.Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();

            if (ipv4 == null)
                throw new Exception("Endereço IPv4 não encontrado");

            return ipv4.Address.ToString();
        }

        private void ShowMessageBox(string title, string message, MessageBoxIcon icon) {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, title, buttons, icon);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            if (socket != null)
                socket.Close();

            Console.WriteLine("Encerrando a aplicação...");
        }

        private void AdicionarMensagem(string mensagem) {
            // Expressão regular para encontrar nomes de usuário, assumindo que eles estão entre colchetes [Nome]
            string pattern = @"\[(.*?)\]";
            MatchCollection matches = Regex.Matches(mensagem, pattern);

            TextBoxHistorico.Text += mensagem + Environment.NewLine;

            // Define a cor para os nomes de usuário
            foreach (Match match in matches) {
                TextBoxHistorico.SelectionStart = TextBoxHistorico.Text.IndexOf(match.Value);
                TextBoxHistorico.SelectionLength = match.Length;
                //TextBoxHistorico.SelectionColor = Color.Blue;
                TextBoxHistorico.Font = new Font(TextBoxHistorico.Font, FontStyle.Bold);
            }
        }

        private void TextBoxHistorico_TextChanged(object sender, EventArgs e) {
            // Mantém o foco no final do texto
            TextBoxHistorico.SelectionStart = TextBoxHistorico.Text.Length;
            TextBoxHistorico.ScrollToCaret();
        }

        private void InitializeSocket() {
            // Cria um socket TCP
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var enderecoIPLocal = IPAddress.Parse(RecuperarEnderecoIPLocal());

            // Define a porta local e o endereço IP
            socket.Bind(new IPEndPoint(enderecoIPLocal, 6969));
        }
    }
}