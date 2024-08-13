using System.ComponentModel;

namespace RecepciónPesosJamesBrown.Controllers
{
    public class VentanaController : INotifyPropertyChanged
    {
        TextBox txtBal1Pmin;
        TextBox txtBal2Pmin;
        TextBox txtBal3Pmin;
        TextBox txtBal4Pmin;
        TextBox txtBal1Pmax;
        TextBox txtBal2Pmax;
        TextBox txtBal3Pmax;
        TextBox txtBal4Pmax;
        TextBox txtBal1Tolmin;
        TextBox txtBal2Tolmin;
        TextBox txtBal3Tolmin;
        TextBox txtBal4Tolmin;
        TextBox txtBal1Tolmax;
        TextBox txtBal2Tolmax;
        TextBox txtBal3Tolmax;
        TextBox txtBal4Tolmax;
        ProgressBar prgCache;

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _seCargaronLosDatos;

        public VentanaController(TextBox txtBal1Pmin, TextBox txtBal1Pmax, TextBox txtBal1Tolmin, TextBox txtBal1Tolmax,
            TextBox txtBal2Pmin, TextBox txtBal2Pmax, TextBox txtBal2Tolmin, TextBox txtBal2Tolmax,
            TextBox txtBal3Pmin, TextBox txtBal3Pmax, TextBox txtBal3Tolmin, TextBox txtBal3Tolmax,
            TextBox txtBal4Pmin, TextBox txtBal4Pmax, TextBox txtBal4Tolmin, TextBox txtBal4Tolmax, ProgressBar prgCache)
        {
            this.txtBal1Pmax = txtBal1Pmax;
            this.txtBal2Pmax = txtBal2Pmax;
            this.txtBal3Pmax = txtBal3Pmax;
            this.txtBal4Pmax = txtBal4Pmax;

            this.txtBal1Pmin = txtBal1Pmin;
            this.txtBal2Pmin = txtBal2Pmin;
            this.txtBal3Pmin = txtBal3Pmin;
            this.txtBal4Pmin = txtBal4Pmin;

            this.txtBal1Tolmin = txtBal1Tolmin;
            this.txtBal2Tolmin = txtBal2Tolmin;
            this.txtBal3Tolmin = txtBal3Tolmin;
            this.txtBal4Tolmin = txtBal4Tolmin;

            this.txtBal1Tolmax = txtBal1Tolmax;
            this.txtBal2Tolmax = txtBal2Tolmax;
            this.txtBal3Tolmax = txtBal3Tolmax;
            this.txtBal4Tolmax = txtBal4Tolmax;
            this.prgCache = prgCache;
        }
        public void GuardarCambios()
        {
            ConfiguracionBalanzas.Default.Bal1_Pmin = float.Parse(txtBal1Pmin.Text);
            ConfiguracionBalanzas.Default.Bal1_Pmax = float.Parse(txtBal1Pmax.Text);
            ConfiguracionBalanzas.Default.Bal1_Tolmin = float.Parse(txtBal1Tolmin.Text);
            ConfiguracionBalanzas.Default.Bal1_Tolmax = float.Parse(txtBal1Tolmax.Text);
            ConfiguracionBalanzas.Default.Bal2_Pmin = float.Parse(txtBal2Pmin.Text);
            ConfiguracionBalanzas.Default.Bal2_Pmax = float.Parse(txtBal2Pmax.Text);
            ConfiguracionBalanzas.Default.Bal2_Tolmin = float.Parse(txtBal2Tolmin.Text);
            ConfiguracionBalanzas.Default.Bal2_Tolmax = float.Parse(txtBal2Tolmax.Text);
            ConfiguracionBalanzas.Default.Bal3_Pmin = float.Parse(txtBal3Pmin.Text);
            ConfiguracionBalanzas.Default.Bal3_Pmax = float.Parse(txtBal3Pmax.Text);
            ConfiguracionBalanzas.Default.Bal3_Tolmin = float.Parse(txtBal3Tolmin.Text);
            ConfiguracionBalanzas.Default.Bal3_Tolmax = float.Parse(txtBal3Tolmax.Text);
            ConfiguracionBalanzas.Default.Bal4_Pmin = float.Parse(txtBal4Pmin.Text);
            ConfiguracionBalanzas.Default.Bal4_Pmax = float.Parse(txtBal4Pmax.Text);
            ConfiguracionBalanzas.Default.Bal4_Tolmin = float.Parse(txtBal4Tolmin.Text);
            ConfiguracionBalanzas.Default.Bal4_Tolmax = float.Parse(txtBal4Tolmax.Text);

            ConfiguracionBalanzas.Default.Save();
        }
        public void CargarBalanzas()
        {
            txtBal1Pmin.Text = ConfiguracionBalanzas.Default.Bal1_Pmin.ToString();
            txtBal1Pmax.Text = ConfiguracionBalanzas.Default.Bal1_Pmax.ToString();
            txtBal1Tolmin.Text = ConfiguracionBalanzas.Default.Bal1_Tolmin.ToString();
            txtBal1Tolmax.Text = ConfiguracionBalanzas.Default.Bal1_Tolmax.ToString();

            txtBal2Pmin.Text = ConfiguracionBalanzas.Default.Bal2_Pmin.ToString();
            txtBal2Pmax.Text = ConfiguracionBalanzas.Default.Bal2_Pmax.ToString();
            txtBal2Tolmin.Text = ConfiguracionBalanzas.Default.Bal2_Tolmin.ToString();
            txtBal2Tolmax.Text = ConfiguracionBalanzas.Default.Bal2_Tolmax.ToString();

            txtBal3Pmin.Text = ConfiguracionBalanzas.Default.Bal3_Pmin.ToString();
            txtBal3Pmax.Text = ConfiguracionBalanzas.Default.Bal3_Pmax.ToString();
            txtBal3Tolmin.Text = ConfiguracionBalanzas.Default.Bal3_Tolmin.ToString();
            txtBal3Tolmax.Text = ConfiguracionBalanzas.Default.Bal3_Tolmax.ToString();

            txtBal4Pmin.Text = ConfiguracionBalanzas.Default.Bal4_Pmin.ToString();
            txtBal4Pmax.Text = ConfiguracionBalanzas.Default.Bal4_Pmax.ToString();
            txtBal4Tolmin.Text = ConfiguracionBalanzas.Default.Bal4_Tolmin.ToString();
            txtBal4Tolmax.Text = ConfiguracionBalanzas.Default.Bal4_Tolmax.ToString();
        }
        public bool SeCargaronLosDatos
        {
            get { return _seCargaronLosDatos; }
            set
            {
                if (_seCargaronLosDatos != value)
                {
                    _seCargaronLosDatos = value;
                    OnPropertyChanged(nameof(SeCargaronLosDatos));
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
