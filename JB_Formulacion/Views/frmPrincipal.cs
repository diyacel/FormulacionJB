using JB_Formulacion.Controllers;
using JB_Formulacion.Helper;
using JB_Formulacion.Properties;
using JB_Formulacion.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JB_Formulacion
{
    public partial class frmPrincipal : Form
    {
        BalanceComunication comunication;
        public frmPrincipal()
        {
            InitializeComponent();
            abrirForm(new frmSobre());
            strpEstado.Text = "Desactivado";
        }
        public void abrirForm(Form form)
        {
            while (pnlPrincipal.Controls.Count > 0)
            {
                pnlPrincipal.Controls.RemoveAt(index: 0);
            }
            Form formHijo = form;
            form.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            pnlPrincipal.Controls.Add(formHijo);
            formHijo.Show();
        }

        private void mnuSobre_Click(object sender, EventArgs e)
        {
            abrirForm(new frmSobre());
        }

        private void mnuConfiguracion_Click(object sender, EventArgs e)
        {
            abrirForm(new frmConfiguracion());
        }


        private async void strpbtnEstado_Click(object sender, EventArgs e)
        {
            if (strpbtnEstado.Text.Equals("Activado"))
            {
                MessageBox.Show("El receptor ya se encuentra activado");
            }
            else
            {
                strpEstado.Text = "Activado";
                strpEstado.BackColor = Color.LimeGreen;
                strpbtnEstado.Image = Resources.cheque;
                await IniciarRecepcionAsync();
            }
        }
        public async Task IniciarRecepcionAsync()
        {
            comunication = new BalanceComunication();
            //await comunication.pruebaAsync(5);
        }


    }
}
