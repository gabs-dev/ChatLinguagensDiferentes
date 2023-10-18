using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

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
                ShowMessageBox("Campo vazio!", "O endereço IP do servidor é obrigtório.", MessageBoxIcon.Warning);
                return;
            }

            // Conecta ao servidor
            socket.Connect(enderecoIPServidor, 6969);

            if (!socket.Connected)
                ShowMessageBox("Erro ao conectar!", "Não foi possível conectar com o servidor.", MessageBoxIcon.Error);

            ShowMessageBox("Sucesso!", "Conectado.", MessageBoxIcon.Information);

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
                    //TextBoxHistorico.Text += $"[Pedro]: {mensagem}\n";
                    AdicionarMensagem($"[Pedro]: {mensagem}", TipoMensagem.RECEIVED);
                }
            }).Start();
        }

        private void ButtonEnviarMensagem_Click(object sender, EventArgs e) {
            EnviarMensagem();
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

        private void AdicionarMensagem(string mensagem, TipoMensagem tipo) {
            string usuario = mensagem.Split(":")[0];
            string conteudoMensagem = mensagem.Split(":")[1];
            var corNomeUsuario = Color.Black;

            if (tipo == TipoMensagem.SENT)
                corNomeUsuario = Color.BlueViolet;
            else if (tipo == TipoMensagem.RECEIVED)
                corNomeUsuario = Color.DarkOrange;

            TextBoxHistorico.SelectionStart = TextBoxHistorico.TextLength;
            TextBoxHistorico.SelectionLength = 0;

            TextBoxHistorico.SelectionColor = corNomeUsuario;
            TextBoxHistorico.SelectionFont = new Font(TextBoxHistorico.Font, FontStyle.Bold);
            TextBoxHistorico.AppendText(usuario + ": ");

            TextBoxHistorico.SelectionColor = Color.Black;
            TextBoxHistorico.SelectionFont = new Font(TextBoxHistorico.Font, FontStyle.Regular);
            TextBoxHistorico.AppendText(conteudoMensagem + Environment.NewLine);

            TextBoxHistorico.SelectionStart = TextBoxHistorico.TextLength;
            TextBoxHistorico.ScrollToCaret();
        }

        private void TextBoxMensagem_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                EnviarMensagem();

                // Impede a inserção de quebra de linha
                e.Handled = true;
            }
        }

        private void EnviarMensagem() {
            if (socket == null || !socket.Connected) {
                ShowMessageBox("Erro de conexão!", "Nenhuma conexão via socket foi inicializada. " +
                    "Primeiro estabeleça conexão com o outro participante.", MessageBoxIcon.Error);
                return;
            }

            string mensagem = TextBoxMensagem.Text;

            if (mensagem.Trim().Length == 0)
                return;

            // Envia a mensagem
            socket.Send(Encoding.UTF8.GetBytes(mensagem));

            // Adiciona a minha mensagem ao histórico e limpa e campo de mensagem
            AdicionarMensagem($"[Eu]: {mensagem}", TipoMensagem.SENT);
            TextBoxMensagem.Text = "";
            TextBoxMensagem.Focus();
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e) {
            if (socket != null)
                socket.Close();

            Console.WriteLine("Encerrando a aplicação...");
        }
    }
}