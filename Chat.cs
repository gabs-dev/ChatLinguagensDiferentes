using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace ChatLinguagensDiferentes {
    public partial class Chat : Form 
    {
        private Thread _threadReceberMensagem;
        private SocketService _socketService;

        public Chat() {
            InitializeComponent();
            _socketService = new SocketService();
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
            _socketService.ConnectToServer(enderecoIPServidor);

            if (!_socketService.ServerSocket.Connected)
                ShowMessageBox("Erro ao conectar!", "Não foi possível conectar com o servidor.", MessageBoxIcon.Error);

            ShowMessageBox("Sucesso!", "Conectado.", MessageBoxIcon.Information);
        }

        private void ButtonEnviarMensagem_Click(object sender, EventArgs e) {
            EnviarMensagem();
        }

        private void ShowMessageBox(string title, string message, MessageBoxIcon icon) {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, title, buttons, icon);
        }

        private void InitListenerThread() {
            if (_threadReceberMensagem == null) {
                _threadReceberMensagem = new Thread(ReceberMensagem);
                _threadReceberMensagem.Start();
            }

            if (!_threadReceberMensagem.IsAlive)
                _threadReceberMensagem.Start();
        }

        private void ReceberMensagem() {
            while (true) {
                try {
                    Socket clientSocket = _socketService.ListenerSocket.Accept();

                    // Recebendo uma mensagem
                    byte[] bytes = new byte[2048];
                    int bytesRead = clientSocket.Receive(bytes);

                    // Convertendo a mensagem em string
                    string mensagem = Encoding.UTF8.GetString(bytes).Trim();

                    // Exibe a mensagem
                    AdicionarMensagem($"[Pedro]: {mensagem}", TipoMensagem.RECEIVED);

                    clientSocket.Close();
                } catch (SocketException e) {
                    Console.WriteLine(e.ToString());
                } catch (ObjectDisposedException e) {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void AdicionarMensagem(string mensagem, TipoMensagem tipo) {
            string usuario = mensagem.Split(":")[0];
            string conteudoMensagem = mensagem.Split(":")[1];
            var corNomeUsuario = Color.Black;

            if (tipo == TipoMensagem.SENT)
                corNomeUsuario = Color.BlueViolet;
            else if (tipo == TipoMensagem.RECEIVED)
                corNomeUsuario = Color.DarkOrange;

            TextBoxHistorico.Invoke((MethodInvoker)delegate {
                TextBoxHistorico.SelectionStart = TextBoxHistorico.TextLength;
                TextBoxHistorico.SelectionLength = 0;

                TextBoxHistorico.SelectionColor = corNomeUsuario;
                TextBoxHistorico.SelectionFont = new Font(TextBoxHistorico.Font, FontStyle.Bold);
                TextBoxHistorico.AppendText(usuario + ": ");

                TextBoxHistorico.SelectionColor = Color.Black;
                TextBoxHistorico.SelectionFont = new Font(TextBoxHistorico.Font, FontStyle.Regular);
                TextBoxHistorico.AppendText(conteudoMensagem);
                TextBoxHistorico.AppendText(Environment.NewLine);

                TextBoxHistorico.SelectionStart = TextBoxHistorico.TextLength;
                TextBoxHistorico.ScrollToCaret();
            });
        }

        private void TextBoxMensagem_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                EnviarMensagem();

                // Impede a inserção de quebra de linha
                e.Handled = true;
            }
        }

        private void EnviarMensagem() {
            string mensagem = TextBoxMensagem.Text;

            if (mensagem.Trim().Length == 0)
                return;

            if (_socketService.ServerSocket == null || !_socketService.ServerSocket.Connected) {
                ShowMessageBox("Erro de conexão!", "Nenhuma conexão via socket foi inicializada. " +
                    "Primeiro estabeleça conexão com o outro participante.", MessageBoxIcon.Error);
                return;
            }

            // Envia a mensagem
            _socketService.SendMessage(mensagem);

            // Adiciona a minha mensagem ao histórico e limpa e campo de mensagem
            AdicionarMensagem($"[Eu]: {mensagem}", TipoMensagem.SENT);
            TextBoxMensagem.Text = "";
            TextBoxMensagem.Focus();

            InitListenerThread();
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e) {
            _socketService.CloseAllConnections();

            if (_threadReceberMensagem != null)
                _threadReceberMensagem.Interrupt();

            Console.WriteLine("Encerrando a aplicação...");
        }
    }
}