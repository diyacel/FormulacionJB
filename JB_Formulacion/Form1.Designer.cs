namespace RecepciónPesosJamesBrown
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
            tbReceptor = new TabControl();
            tbPresentacion = new TabPage();
            txtLog = new ListBox();
            btnIniciar = new Button();
            tbTolerancias = new TabPage();
            btnGuardarConfiguracion = new Button();
            groupBox4 = new GroupBox();
            txtBal4Tolmax = new TextBox();
            label13 = new Label();
            txtBal4Tolmin = new TextBox();
            label14 = new Label();
            txtBal4Pmax = new TextBox();
            label15 = new Label();
            txtBal4Pmin = new TextBox();
            label16 = new Label();
            groupBox3 = new GroupBox();
            txtBal3Tolmax = new TextBox();
            label9 = new Label();
            txtBal3Tolmin = new TextBox();
            label10 = new Label();
            txtBal3Pmax = new TextBox();
            label11 = new Label();
            txtBal3Pmin = new TextBox();
            label12 = new Label();
            groupBox2 = new GroupBox();
            txtBal2Tolmax = new TextBox();
            label5 = new Label();
            txtBal2Tolmin = new TextBox();
            label6 = new Label();
            txtBal2Pmax = new TextBox();
            label7 = new Label();
            txtBal2Pmin = new TextBox();
            label8 = new Label();
            groupBox1 = new GroupBox();
            txtBal1Tolmax = new TextBox();
            label4 = new Label();
            txtBal1Tolmin = new TextBox();
            label3 = new Label();
            txtBal1Pmax = new TextBox();
            label2 = new Label();
            txtBal1Pmin = new TextBox();
            label1 = new Label();
            tbConfiguracion = new TabPage();
            prgCache = new ProgressBar();
            label17 = new Label();
            tbReceptor.SuspendLayout();
            tbPresentacion.SuspendLayout();
            tbTolerancias.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tbConfiguracion.SuspendLayout();
            SuspendLayout();
            // 
            // tbReceptor
            // 
            tbReceptor.Controls.Add(tbPresentacion);
            tbReceptor.Controls.Add(tbTolerancias);
            tbReceptor.Controls.Add(tbConfiguracion);
            tbReceptor.Dock = DockStyle.Fill;
            tbReceptor.Location = new Point(0, 0);
            tbReceptor.Name = "tbReceptor";
            tbReceptor.SelectedIndex = 0;
            tbReceptor.Size = new Size(608, 459);
            tbReceptor.TabIndex = 3;
            // 
            // tbPresentacion
            // 
            tbPresentacion.Controls.Add(txtLog);
            tbPresentacion.Controls.Add(btnIniciar);
            tbPresentacion.Location = new Point(4, 29);
            tbPresentacion.Name = "tbPresentacion";
            tbPresentacion.Padding = new Padding(3);
            tbPresentacion.Size = new Size(600, 426);
            tbPresentacion.TabIndex = 0;
            tbPresentacion.Text = "Recepción";
            tbPresentacion.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            txtLog.FormattingEnabled = true;
            txtLog.Location = new Point(30, 29);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(538, 204);
            txtLog.TabIndex = 2;
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(215, 278);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(94, 29);
            btnIniciar.TabIndex = 1;
            btnIniciar.Text = "INICIAR";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // tbTolerancias
            // 
            tbTolerancias.Controls.Add(btnGuardarConfiguracion);
            tbTolerancias.Controls.Add(groupBox4);
            tbTolerancias.Controls.Add(groupBox3);
            tbTolerancias.Controls.Add(groupBox2);
            tbTolerancias.Controls.Add(groupBox1);
            tbTolerancias.Location = new Point(4, 29);
            tbTolerancias.Name = "tbTolerancias";
            tbTolerancias.Padding = new Padding(3);
            tbTolerancias.Size = new Size(600, 426);
            tbTolerancias.TabIndex = 1;
            tbTolerancias.Text = "Tolerancias";
            tbTolerancias.UseVisualStyleBackColor = true;
            tbTolerancias.Enter += tbConfiguracion_Enter;
            // 
            // btnGuardarConfiguracion
            // 
            btnGuardarConfiguracion.Location = new Point(163, 381);
            btnGuardarConfiguracion.Name = "btnGuardarConfiguracion";
            btnGuardarConfiguracion.Size = new Size(246, 29);
            btnGuardarConfiguracion.TabIndex = 4;
            btnGuardarConfiguracion.Text = "Guardar Configuración";
            btnGuardarConfiguracion.UseVisualStyleBackColor = true;
            btnGuardarConfiguracion.Click += btnGuardarConfiguracion_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(txtBal4Tolmax);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(txtBal4Tolmin);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(txtBal4Pmax);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(txtBal4Pmin);
            groupBox4.Controls.Add(label16);
            groupBox4.Location = new Point(283, 192);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(269, 180);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Balanza 4";
            // 
            // txtBal4Tolmax
            // 
            txtBal4Tolmax.Location = new Point(184, 139);
            txtBal4Tolmax.Name = "txtBal4Tolmax";
            txtBal4Tolmax.Size = new Size(75, 27);
            txtBal4Tolmax.TabIndex = 7;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(25, 146);
            label13.Name = "label13";
            label13.Size = new Size(156, 20);
            label13.TabIndex = 6;
            label13.Text = "Tolerancia maxima(g):";
            // 
            // txtBal4Tolmin
            // 
            txtBal4Tolmin.Location = new Point(184, 103);
            txtBal4Tolmin.Name = "txtBal4Tolmin";
            txtBal4Tolmin.Size = new Size(75, 27);
            txtBal4Tolmin.TabIndex = 5;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(25, 110);
            label14.Name = "label14";
            label14.Size = new Size(153, 20);
            label14.TabIndex = 4;
            label14.Text = "Tolerancia mínima(g):";
            // 
            // txtBal4Pmax
            // 
            txtBal4Pmax.Location = new Point(184, 65);
            txtBal4Pmax.Name = "txtBal4Pmax";
            txtBal4Pmax.Size = new Size(75, 27);
            txtBal4Pmax.TabIndex = 3;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(25, 69);
            label15.Name = "label15";
            label15.Size = new Size(126, 20);
            label15.TabIndex = 2;
            label15.Text = "Peso maximo(kg):";
            // 
            // txtBal4Pmin
            // 
            txtBal4Pmin.Location = new Point(184, 32);
            txtBal4Pmin.Name = "txtBal4Pmin";
            txtBal4Pmin.Size = new Size(75, 27);
            txtBal4Pmin.TabIndex = 1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(25, 35);
            label16.Name = "label16";
            label16.Size = new Size(123, 20);
            label16.TabIndex = 0;
            label16.Text = "Peso mínimo(kg):";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtBal3Tolmax);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(txtBal3Tolmin);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(txtBal3Pmax);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(txtBal3Pmin);
            groupBox3.Controls.Add(label12);
            groupBox3.Location = new Point(8, 192);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(269, 180);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Balanza 3";
            // 
            // txtBal3Tolmax
            // 
            txtBal3Tolmax.Location = new Point(184, 139);
            txtBal3Tolmax.Name = "txtBal3Tolmax";
            txtBal3Tolmax.Size = new Size(75, 27);
            txtBal3Tolmax.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(25, 146);
            label9.Name = "label9";
            label9.Size = new Size(156, 20);
            label9.TabIndex = 6;
            label9.Text = "Tolerancia maxima(g):";
            // 
            // txtBal3Tolmin
            // 
            txtBal3Tolmin.Location = new Point(184, 103);
            txtBal3Tolmin.Name = "txtBal3Tolmin";
            txtBal3Tolmin.Size = new Size(75, 27);
            txtBal3Tolmin.TabIndex = 5;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(25, 110);
            label10.Name = "label10";
            label10.Size = new Size(153, 20);
            label10.TabIndex = 4;
            label10.Text = "Tolerancia mínima(g):";
            // 
            // txtBal3Pmax
            // 
            txtBal3Pmax.Location = new Point(184, 65);
            txtBal3Pmax.Name = "txtBal3Pmax";
            txtBal3Pmax.Size = new Size(75, 27);
            txtBal3Pmax.TabIndex = 3;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(25, 69);
            label11.Name = "label11";
            label11.Size = new Size(126, 20);
            label11.TabIndex = 2;
            label11.Text = "Peso maximo(kg):";
            // 
            // txtBal3Pmin
            // 
            txtBal3Pmin.Location = new Point(184, 32);
            txtBal3Pmin.Name = "txtBal3Pmin";
            txtBal3Pmin.Size = new Size(75, 27);
            txtBal3Pmin.TabIndex = 1;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(25, 35);
            label12.Name = "label12";
            label12.Size = new Size(123, 20);
            label12.TabIndex = 0;
            label12.Text = "Peso mínimo(kg):";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtBal2Tolmax);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtBal2Tolmin);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(txtBal2Pmax);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(txtBal2Pmin);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(283, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(269, 180);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Balanza 2";
            // 
            // txtBal2Tolmax
            // 
            txtBal2Tolmax.Location = new Point(184, 139);
            txtBal2Tolmax.Name = "txtBal2Tolmax";
            txtBal2Tolmax.Size = new Size(75, 27);
            txtBal2Tolmax.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 146);
            label5.Name = "label5";
            label5.Size = new Size(156, 20);
            label5.TabIndex = 6;
            label5.Text = "Tolerancia maxima(g):";
            // 
            // txtBal2Tolmin
            // 
            txtBal2Tolmin.Location = new Point(184, 103);
            txtBal2Tolmin.Name = "txtBal2Tolmin";
            txtBal2Tolmin.Size = new Size(75, 27);
            txtBal2Tolmin.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(25, 110);
            label6.Name = "label6";
            label6.Size = new Size(153, 20);
            label6.TabIndex = 4;
            label6.Text = "Tolerancia mínima(g):";
            // 
            // txtBal2Pmax
            // 
            txtBal2Pmax.Location = new Point(184, 65);
            txtBal2Pmax.Name = "txtBal2Pmax";
            txtBal2Pmax.Size = new Size(75, 27);
            txtBal2Pmax.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(25, 69);
            label7.Name = "label7";
            label7.Size = new Size(126, 20);
            label7.TabIndex = 2;
            label7.Text = "Peso maximo(kg):";
            // 
            // txtBal2Pmin
            // 
            txtBal2Pmin.Location = new Point(184, 32);
            txtBal2Pmin.Name = "txtBal2Pmin";
            txtBal2Pmin.Size = new Size(75, 27);
            txtBal2Pmin.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(25, 35);
            label8.Name = "label8";
            label8.Size = new Size(123, 20);
            label8.TabIndex = 0;
            label8.Text = "Peso mínimo(kg):";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtBal1Tolmax);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtBal1Tolmin);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtBal1Pmax);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtBal1Pmin);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(8, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(269, 180);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Balanza 1";
            // 
            // txtBal1Tolmax
            // 
            txtBal1Tolmax.Location = new Point(184, 139);
            txtBal1Tolmax.Name = "txtBal1Tolmax";
            txtBal1Tolmax.Size = new Size(75, 27);
            txtBal1Tolmax.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 146);
            label4.Name = "label4";
            label4.Size = new Size(156, 20);
            label4.TabIndex = 6;
            label4.Text = "Tolerancia maxima(g):";
            // 
            // txtBal1Tolmin
            // 
            txtBal1Tolmin.Location = new Point(184, 103);
            txtBal1Tolmin.Name = "txtBal1Tolmin";
            txtBal1Tolmin.Size = new Size(75, 27);
            txtBal1Tolmin.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 110);
            label3.Name = "label3";
            label3.Size = new Size(153, 20);
            label3.TabIndex = 4;
            label3.Text = "Tolerancia mínima(g):";
            // 
            // txtBal1Pmax
            // 
            txtBal1Pmax.Location = new Point(184, 65);
            txtBal1Pmax.Name = "txtBal1Pmax";
            txtBal1Pmax.Size = new Size(75, 27);
            txtBal1Pmax.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 69);
            label2.Name = "label2";
            label2.Size = new Size(126, 20);
            label2.TabIndex = 2;
            label2.Text = "Peso maximo(kg):";
            // 
            // txtBal1Pmin
            // 
            txtBal1Pmin.Location = new Point(184, 32);
            txtBal1Pmin.Name = "txtBal1Pmin";
            txtBal1Pmin.Size = new Size(75, 27);
            txtBal1Pmin.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 35);
            label1.Name = "label1";
            label1.Size = new Size(123, 20);
            label1.TabIndex = 0;
            label1.Text = "Peso mínimo(kg):";
            // 
            // tbConfiguracion
            // 
            tbConfiguracion.Controls.Add(label17);
            tbConfiguracion.Controls.Add(prgCache);
            tbConfiguracion.Location = new Point(4, 29);
            tbConfiguracion.Name = "tbConfiguracion";
            tbConfiguracion.Padding = new Padding(3);
            tbConfiguracion.Size = new Size(600, 426);
            tbConfiguracion.TabIndex = 2;
            tbConfiguracion.Text = "Configuración";
            tbConfiguracion.UseVisualStyleBackColor = true;
            // 
            // prgCache
            // 
            prgCache.Location = new Point(17, 50);
            prgCache.Name = "prgCache";
            prgCache.Size = new Size(197, 29);
            prgCache.TabIndex = 1;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(17, 22);
            label17.Name = "label17";
            label17.Size = new Size(197, 20);
            label17.TabIndex = 2;
            label17.Text = "Cargando Ordenes en Cache";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 459);
            Controls.Add(tbReceptor);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            tbReceptor.ResumeLayout(false);
            tbPresentacion.ResumeLayout(false);
            tbTolerancias.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tbConfiguracion.ResumeLayout(false);
            tbConfiguracion.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tbReceptor;
        private TabPage tbPresentacion;
        private TabPage tbTolerancias;
        private Button btnIniciar;
        private ListBox txtLog;
        private GroupBox groupBox1;
        private Label label2;
        private TextBox txtBal1Pmin;
        private Label label1;
        private Label label4;
        private TextBox txtBal1Tolmin;
        private Label label3;
        private TextBox txtBal1Pmax;
        private TextBox txtBal1Tolmax;
        private GroupBox groupBox2;
        private TextBox txtBal2Tolmax;
        private Label label5;
        private TextBox txtBal2Tolmin;
        private Label label6;
        private TextBox txtBal2Pmax;
        private Label label7;
        private TextBox txtBal2Pmin;
        private Label label8;
        private GroupBox groupBox4;
        private TextBox txtBal4Tolmax;
        private Label label13;
        private TextBox txtBal4Tolmin;
        private Label label14;
        private TextBox txtBal4Pmax;
        private Label label15;
        private TextBox txtBal4Pmin;
        private Label label16;
        private GroupBox groupBox3;
        private TextBox txtBal3Tolmax;
        private Label label9;
        private TextBox txtBal3Tolmin;
        private Label label10;
        private TextBox txtBal3Pmax;
        private Label label11;
        private TextBox txtBal3Pmin;
        private Label label12;
        private Button btnGuardarConfiguracion;
        private TabPage tbConfiguracion;
        private Label label17;
        private ProgressBar prgCache;
    }
}
