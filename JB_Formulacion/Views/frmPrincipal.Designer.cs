namespace JB_Formulacion
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlPrincipal = new Panel();
            menuStrip1 = new MenuStrip();
            mnuSobre = new ToolStripMenuItem();
            mnuConfiguracion = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            strpbtnEstado = new ToolStripButton();
            strpEstado = new ToolStripLabel();
            notifyIcon1 = new NotifyIcon(components);
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 28);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(662, 448);
            pnlPrincipal.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuSobre, mnuConfiguracion });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(662, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuSobre
            // 
            mnuSobre.Name = "mnuSobre";
            mnuSobre.Size = new Size(123, 24);
            mnuSobre.Text = "Sobre nosotros";
            mnuSobre.Click += mnuSobre_Click;
            // 
            // mnuConfiguracion
            // 
            mnuConfiguracion.Name = "mnuConfiguracion";
            mnuConfiguracion.Size = new Size(116, 24);
            mnuConfiguracion.Text = "Configuración";
            mnuConfiguracion.Click += mnuConfiguracion_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { strpbtnEstado, strpEstado });
            toolStrip1.Location = new Point(0, 449);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(662, 27);
            toolStrip1.TabIndex = 4;
            toolStrip1.Text = "toolStrip1";
            // 
            // strpbtnEstado
            // 
            strpbtnEstado.DisplayStyle = ToolStripItemDisplayStyle.Image;
            strpbtnEstado.Image = Properties.Resources.garrapata;
            strpbtnEstado.ImageTransparentColor = Color.Magenta;
            strpbtnEstado.Name = "strpbtnEstado";
            strpbtnEstado.Size = new Size(29, 24);
            strpbtnEstado.Text = "toolStripButton1";
            strpbtnEstado.Click += strpbtnEstado_Click;
            // 
            // strpEstado
            // 
            strpEstado.ActiveLinkColor = Color.Green;
            strpEstado.BackColor = SystemColors.Control;
            strpEstado.ForeColor = SystemColors.ActiveCaptionText;
            strpEstado.Name = "strpEstado";
            strpEstado.Size = new Size(91, 24);
            strpEstado.Text = "Desactivado";
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(662, 476);
            Controls.Add(toolStrip1);
            Controls.Add(pnlPrincipal);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "frmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel pnlPrincipal;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuSobre;
        private ToolStripMenuItem mnuConfiguracion;
        private ToolStrip toolStrip1;
        private ToolStripLabel strpEstado;
        private ToolStripButton strpbtnEstado;
        private NotifyIcon notifyIcon1;
    }
}