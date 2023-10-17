namespace ChatLinguagensDiferentes {
    partial class Chat {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chat));
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBoxInformacoesServidor = new System.Windows.Forms.GroupBox();
            this.ButtonConectar = new System.Windows.Forms.Button();
            this.TextBoxEnderecoServidor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TextBoxHistorico = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBoxMensagem = new System.Windows.Forms.TextBox();
            this.ButtonEnviarMensagem = new System.Windows.Forms.Button();
            this.GroupBoxInformacoesServidor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(83, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "SdChat";
            // 
            // GroupBoxInformacoesServidor
            // 
            this.GroupBoxInformacoesServidor.Controls.Add(this.ButtonConectar);
            this.GroupBoxInformacoesServidor.Controls.Add(this.TextBoxEnderecoServidor);
            this.GroupBoxInformacoesServidor.Controls.Add(this.label2);
            this.GroupBoxInformacoesServidor.Location = new System.Drawing.Point(295, 12);
            this.GroupBoxInformacoesServidor.Name = "GroupBoxInformacoesServidor";
            this.GroupBoxInformacoesServidor.Size = new System.Drawing.Size(401, 58);
            this.GroupBoxInformacoesServidor.TabIndex = 1;
            this.GroupBoxInformacoesServidor.TabStop = false;
            this.GroupBoxInformacoesServidor.Text = "Informações do 2º participante";
            // 
            // ButtonConectar
            // 
            this.ButtonConectar.Location = new System.Drawing.Point(309, 25);
            this.ButtonConectar.Name = "ButtonConectar";
            this.ButtonConectar.Size = new System.Drawing.Size(75, 23);
            this.ButtonConectar.TabIndex = 2;
            this.ButtonConectar.Text = "Conectar";
            this.ButtonConectar.UseVisualStyleBackColor = true;
            this.ButtonConectar.Click += new System.EventHandler(this.ButtonConectar_Click);
            // 
            // TextBoxEnderecoServidor
            // 
            this.TextBoxEnderecoServidor.Location = new System.Drawing.Point(81, 26);
            this.TextBoxEnderecoServidor.Name = "TextBoxEnderecoServidor";
            this.TextBoxEnderecoServidor.Size = new System.Drawing.Size(222, 23);
            this.TextBoxEnderecoServidor.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Endereço IP";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 66);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // TextBoxHistorico
            // 
            this.TextBoxHistorico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextBoxHistorico.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextBoxHistorico.Location = new System.Drawing.Point(0, 22);
            this.TextBoxHistorico.Multiline = true;
            this.TextBoxHistorico.Name = "TextBoxHistorico";
            this.TextBoxHistorico.ReadOnly = true;
            this.TextBoxHistorico.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxHistorico.Size = new System.Drawing.Size(684, 266);
            this.TextBoxHistorico.TabIndex = 3;
            this.TextBoxHistorico.TextChanged += new System.EventHandler(this.TextBoxHistorico_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxHistorico);
            this.groupBox1.Location = new System.Drawing.Point(12, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(684, 288);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mensagens";
            // 
            // TextBoxMensagem
            // 
            this.TextBoxMensagem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextBoxMensagem.Location = new System.Drawing.Point(12, 392);
            this.TextBoxMensagem.Multiline = true;
            this.TextBoxMensagem.Name = "TextBoxMensagem";
            this.TextBoxMensagem.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.TextBoxMensagem.Size = new System.Drawing.Size(625, 46);
            this.TextBoxMensagem.TabIndex = 5;
            // 
            // ButtonEnviarMensagem
            // 
            this.ButtonEnviarMensagem.BackColor = System.Drawing.Color.LightGreen;
            this.ButtonEnviarMensagem.Image = ((System.Drawing.Image)(resources.GetObject("ButtonEnviarMensagem.Image")));
            this.ButtonEnviarMensagem.Location = new System.Drawing.Point(643, 392);
            this.ButtonEnviarMensagem.Name = "ButtonEnviarMensagem";
            this.ButtonEnviarMensagem.Size = new System.Drawing.Size(53, 46);
            this.ButtonEnviarMensagem.TabIndex = 6;
            this.ButtonEnviarMensagem.UseVisualStyleBackColor = false;
            this.ButtonEnviarMensagem.Click += new System.EventHandler(this.ButtonEnviarMensagem_Click);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 450);
            this.Controls.Add(this.ButtonEnviarMensagem);
            this.Controls.Add(this.TextBoxMensagem);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.GroupBoxInformacoesServidor);
            this.Controls.Add(this.label1);
            this.Name = "Chat";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBoxInformacoesServidor.ResumeLayout(false);
            this.GroupBoxInformacoesServidor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private GroupBox GroupBoxInformacoesServidor;
        private Label label2;
        private TextBox TextBoxEnderecoServidor;
        private Button ButtonConectar;
        private PictureBox pictureBox1;
        private TextBox TextBoxHistorico;
        private GroupBox groupBox1;
        private TextBox TextBoxMensagem;
        private Button ButtonEnviarMensagem;
    }
}