using RecepciónPesosJamesBrown.Controllers;
using RecepciónPesosJamesBrown.Helpers;

namespace RecepciónPesosJamesBrown
{
    public partial class Form1 : Form
    {
        BalanceComunication comunication;
        VentanaController controlador;
        public Form1()
        {
            InitializeComponent();
            controlador = new VentanaController(txtBal1Pmin, txtBal1Pmax, txtBal1Tolmin, txtBal1Tolmax,
                txtBal2Pmin, txtBal2Pmax, txtBal2Tolmin, txtBal2Tolmax, txtBal3Pmin, txtBal3Pmax, txtBal3Tolmin,
                txtBal3Tolmax, txtBal4Pmin, txtBal4Pmax, txtBal4Tolmin, txtBal4Tolmax,prgCache);
        }





        public void IniciarRecepcion()
        {
            comunication = new BalanceComunication(txtLog,prgCache);
            MessageBox.Show("Se inició el receptor");
            //await comunication.pruebaAsync(5);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BalanceComunication.hiloSegundoPlano();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            IniciarRecepcion();
        }

        private void btnGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            controlador.GuardarCambios();
            MessageBox.Show("¡Cambios guardados exitosamente!");
        }

        private void tbConfiguracion_Enter(object sender, EventArgs e)
        {
            controlador.CargarBalanzas();
        }
    }
}
