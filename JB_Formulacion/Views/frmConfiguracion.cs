
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JB_Formulacion.Views
{
    public partial class frmConfiguracion : Form
    {
        public frmConfiguracion()
        {
            InitializeComponent();
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            txtBal1Pmin.Text = Settings1.Default.Bal1_Pmin.ToString();
            txtBal1Pmax.Text = Settings1.Default.Bal1_Pmax.ToString();
            txtBal1Tolmin.Text = Settings1.Default.Bal1_Tolmin.ToString();
            txtBal1Tolmax.Text = Settings1.Default.Bal1_Tolmax.ToString();

            txtBal2Pmin.Text = Settings1.Default.Bal2_Pmin.ToString();
            txtBal2Pmax.Text = Settings1.Default.Bal2_Pmax.ToString();
            txtBal2Tolmin.Text = Settings1.Default.Bal2_Tolmin.ToString();
            txtBal2Tolmax.Text = Settings1.Default.Bal2_Tolmax.ToString();

            txtBal3Pmin.Text = Settings1.Default.Bal3_Pmin.ToString();
            txtBal3Pmax.Text = Settings1.Default.Bal3_Pmax.ToString();
            txtBal3Tolmin.Text = Settings1.Default.Bal3_Tolmin.ToString();
            txtBal3Tolmax.Text = Settings1.Default.Bal3_Tolmax.ToString();

            txtBal4Pmin.Text = Settings1.Default.Bal4_Pmin.ToString();
            txtBal4Pmax.Text = Settings1.Default.Bal4_Pmax.ToString();
            txtBal4Tolmin.Text = Settings1.Default.Bal4_Tolmin.ToString();
            txtBal4Tolmax.Text = Settings1.Default.Bal4_Tolmax.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Settings1.Default.Bal1_Pmin=float.Parse(txtBal1Pmin.Text);
            Settings1.Default.Bal1_Pmax = float.Parse(txtBal1Pmax.Text);
            Settings1.Default.Bal1_Tolmin=int.Parse(txtBal1Tolmin.Text);
            Settings1.Default.Bal1_Tolmax=int.Parse(txtBal1Tolmax.Text);
            Settings1.Default.Bal2_Pmin = float.Parse(txtBal2Pmin.Text);
            Settings1.Default.Bal2_Pmax = float.Parse(txtBal2Pmax.Text);
            Settings1.Default.Bal2_Tolmin = int.Parse(txtBal2Tolmin.Text);
            Settings1.Default.Bal2_Tolmax=int.Parse(txtBal2Tolmax.Text);
            Settings1.Default.Bal3_Pmin=float.Parse(txtBal3Pmin.Text);
            Settings1.Default.Bal3_Pmax=float.Parse(txtBal3Pmax.Text);
            Settings1.Default.Bal3_Tolmin = int.Parse(txtBal3Tolmin.Text);
            Settings1.Default.Bal3_Tolmax=int.Parse(txtBal3Tolmax.Text);
            Settings1.Default.Bal4_Pmin=float.Parse(txtBal4Pmin.Text);
            Settings1.Default.Bal4_Pmax=float.Parse(txtBal4Pmax.Text);
            Settings1.Default.Bal4_Tolmin = int.Parse(txtBal4Tolmin.Text);
            Settings1.Default.Bal4_Tolmax=int.Parse(txtBal4Tolmax.Text);

            MessageBox.Show("¡Guardado exitoso!");



            Settings1.Default.Save();
        }
    }
}
