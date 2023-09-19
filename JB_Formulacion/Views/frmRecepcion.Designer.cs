namespace JB_Formulacion.Views
{
    partial class frmRecepcion
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
            btnRecepcion = new Button();
            SuspendLayout();
            // 
            // btnRecepcion
            // 
            btnRecepcion.Location = new Point(108, 121);
            btnRecepcion.Name = "btnRecepcion";
            btnRecepcion.Size = new Size(217, 29);
            btnRecepcion.TabIndex = 0;
            btnRecepcion.Text = "Iniciar Recepcion de datos";
            btnRecepcion.UseVisualStyleBackColor = true;
            btnRecepcion.Click += btnRecepcion_Click;
            // 
            // frmRecepcion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRecepcion);
            Name = "frmRecepcion";
            Text = "frmRecepcion";
            ResumeLayout(false);
        }

        #endregion

        private Button btnRecepcion;
    }
}