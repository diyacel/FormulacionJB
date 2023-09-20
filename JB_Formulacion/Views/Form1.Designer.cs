namespace JB_Formulacion
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOfLiberadas = new Button();
            btnGetMateriasPrimas = new Button();
            listBox1 = new ListBox();
            btnComponentes = new Button();
            txtNumeroOrden = new TextBox();
            btnStock = new Button();
            textBox1 = new TextBox();
            btnCantidadPesada = new Button();
            textBox2 = new TextBox();
            btnLogin = new Button();
            txtNombre = new TextBox();
            txtContraseña = new TextBox();
            btnCompoentesBalanzas = new Button();
            btnNumeroComponente = new Button();
            txtNumComponente = new TextBox();
            SuspendLayout();
            // 
            // btnOfLiberadas
            // 
            btnOfLiberadas.Location = new Point(21, 21);
            btnOfLiberadas.Name = "btnOfLiberadas";
            btnOfLiberadas.Size = new Size(346, 29);
            btnOfLiberadas.TabIndex = 0;
            btnOfLiberadas.Text = "Ordenes liberadas para el pesaje";
            btnOfLiberadas.UseVisualStyleBackColor = true;
            btnOfLiberadas.Click += btnOfLiberadas_Click;
            // 
            // btnGetMateriasPrimas
            // 
            btnGetMateriasPrimas.Location = new Point(29, 77);
            btnGetMateriasPrimas.Name = "btnGetMateriasPrimas";
            btnGetMateriasPrimas.Size = new Size(338, 29);
            btnGetMateriasPrimas.TabIndex = 1;
            btnGetMateriasPrimas.Text = "Materias primas";
            btnGetMateriasPrimas.UseVisualStyleBackColor = true;
            btnGetMateriasPrimas.Click += btnGetMateriasPrimas_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 20;
            listBox1.Location = new Point(422, 21);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(706, 364);
            listBox1.TabIndex = 2;
            // 
            // btnComponentes
            // 
            btnComponentes.Location = new Point(29, 130);
            btnComponentes.Name = "btnComponentes";
            btnComponentes.Size = new Size(338, 29);
            btnComponentes.TabIndex = 3;
            btnComponentes.Text = "Componentes de una orden";
            btnComponentes.UseVisualStyleBackColor = true;
            btnComponentes.Click += btnComponentes_ClickAsync;
            // 
            // txtNumeroOrden
            // 
            txtNumeroOrden.Location = new Point(31, 163);
            txtNumeroOrden.Name = "txtNumeroOrden";
            txtNumeroOrden.Size = new Size(336, 27);
            txtNumeroOrden.TabIndex = 4;
            txtNumeroOrden.Text = "10009132";
            // 
            // btnStock
            // 
            btnStock.Location = new Point(33, 214);
            btnStock.Name = "btnStock";
            btnStock.Size = new Size(334, 29);
            btnStock.TabIndex = 5;
            btnStock.Text = "Transferencia de stock";
            btnStock.UseVisualStyleBackColor = true;
            btnStock.Click += btnStock_ClickAsync;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(36, 251);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(331, 27);
            textBox1.TabIndex = 6;
            // 
            // btnCantidadPesada
            // 
            btnCantidadPesada.Location = new Point(39, 295);
            btnCantidadPesada.Name = "btnCantidadPesada";
            btnCantidadPesada.Size = new Size(328, 29);
            btnCantidadPesada.TabIndex = 7;
            btnCantidadPesada.Text = "Cantidad Pesada";
            btnCantidadPesada.UseVisualStyleBackColor = true;
            btnCantidadPesada.Click += btnCantidadPesada_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(39, 330);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(331, 27);
            textBox2.TabIndex = 8;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(43, 380);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(324, 29);
            btnLogin.TabIndex = 9;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_ClickAsync;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(43, 422);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(125, 27);
            txtNombre.TabIndex = 10;
            txtNombre.Text = "prueba";
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(217, 422);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(125, 27);
            txtContraseña.TabIndex = 11;
            txtContraseña.Text = "prueba";
            // 
            // btnCompoentesBalanzas
            // 
            btnCompoentesBalanzas.Location = new Point(463, 406);
            btnCompoentesBalanzas.Name = "btnCompoentesBalanzas";
            btnCompoentesBalanzas.Size = new Size(225, 29);
            btnCompoentesBalanzas.TabIndex = 12;
            btnCompoentesBalanzas.Text = "Componentes Balanzas";
            btnCompoentesBalanzas.UseVisualStyleBackColor = true;
            btnCompoentesBalanzas.Click += button1_Click;
            // 
            // btnNumeroComponente
            // 
            btnNumeroComponente.Location = new Point(463, 454);
            btnNumeroComponente.Name = "btnNumeroComponente";
            btnNumeroComponente.Size = new Size(225, 29);
            btnNumeroComponente.TabIndex = 13;
            btnNumeroComponente.Text = "Numero de componente";
            btnNumeroComponente.UseVisualStyleBackColor = true;
            btnNumeroComponente.Click += btnNumeroComponente_Click;
            // 
            // txtNumComponente
            // 
            txtNumComponente.Location = new Point(463, 489);
            txtNumComponente.Name = "txtNumComponente";
            txtNumComponente.Size = new Size(125, 27);
            txtNumComponente.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1153, 572);
            Controls.Add(txtNumComponente);
            Controls.Add(btnNumeroComponente);
            Controls.Add(btnCompoentesBalanzas);
            Controls.Add(txtContraseña);
            Controls.Add(txtNombre);
            Controls.Add(btnLogin);
            Controls.Add(textBox2);
            Controls.Add(btnCantidadPesada);
            Controls.Add(textBox1);
            Controls.Add(btnStock);
            Controls.Add(txtNumeroOrden);
            Controls.Add(btnComponentes);
            Controls.Add(listBox1);
            Controls.Add(btnGetMateriasPrimas);
            Controls.Add(btnOfLiberadas);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOfLiberadas;
        private Button btnGetMateriasPrimas;
        private ListBox listBox1;
        private Button btnComponentes;
        private TextBox txtNumeroOrden;
        private Button btnStock;
        private TextBox textBox1;
        private Button btnCantidadPesada;
        private TextBox textBox2;
        private Button btnLogin;
        private TextBox txtNombre;
        private TextBox txtContraseña;
        private Button btnCompoentesBalanzas;
        private Button btnNumeroComponente;
        private TextBox txtNumComponente;
    }
}