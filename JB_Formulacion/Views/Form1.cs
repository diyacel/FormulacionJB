using Azure;
using JB_Formulacion.Controllers;
using JB_Formulacion.Helper;
using JB_Formulacion.Helpers;
using JB_Formulacion.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JB_Formulacion
{
    public partial class Form1 : Form
    {
        ApiController apiController;
        BalanceOptions options;
        public Form1()
        {
            InitializeComponent();
            apiController = new ApiController();
            options = new BalanceOptions();
        }

        private async void btnOfLiberadas_Click(object sender, EventArgs e)
        {
            List<OrdenFabricacion> ordenes = new List<OrdenFabricacion>();
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesFabricacion<List<OrdenFabricacion>>();
            ordenes = (List<OrdenFabricacion>)reply.Data;
            MessageBox.Show(reply.StatusCode);

            listBox1.Items.Clear();
            foreach (OrdenFabricacion orden in ordenes)
            {
                listBox1.Items.Add(orden.NumOrdenFabricacion.ToString());
            }

        }

        private async void btnGetMateriasPrimas_Click(object sender, EventArgs e)
        {
            DataMaterias datamateria = new DataMaterias();
            //List<MateriaPrima> materias = new List<MateriaPrima>();
            //List<ProductoTerminado> productosTerminados = new List<ProductoTerminado>();
            Reply reply = new Reply();
            reply = await apiController.GetMateriaPrima<DataMaterias>();
            datamateria = (DataMaterias)reply.Data;
            MessageBox.Show(reply.StatusCode);

            listBox1.Items.Clear();
            foreach (MateriaPrima materias in datamateria.materiasPrimas)
            {
                listBox1.Items.Add(materias.Codigo + " " + materias.UnidadMedida + " " + materias.Descripcion);
            }
        }
        private async void btnComponentes_ClickAsync(object sender, EventArgs e)
        {
            OrdenComponentes orden = new OrdenComponentes();
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesConComponentes<OrdenComponentes>(txtNumeroOrden.Text);
            orden = (OrdenComponentes)reply.Data;
            MessageBox.Show(reply.StatusCode);
            Balanza balanza = new Balanza();

            listBox1.Items.Clear();
            //listBox1.Items.Add(orden.Componentes)
            foreach (Componente componente in orden.Componentes)
            {
                listBox1.Items.Add(componente.CodigoArticulo.ToString() + " UNIDAD: " + componente.UnidadMedida + " " + componente.Descripcion.ToString());
                foreach (Lote lote in componente.CantidadesPorLote)
                {
                    listBox1.Items.Add(lote.NombreLote.ToString() + " " + lote.Cantidad.ToString());
                }
                listBox1.Items.Add(componente.CantidadPesada.ToString());
                balanza = options.CategorizarPeso(componente);
                listBox1.Items.Add("Numero de balanza: " + balanza.NumeroBalanza.ToString());
                listBox1.Items.Add("Numero de lotes: "+componente.CantidadesPorLote.Count);


            }
        }


        private async void btnStock_ClickAsync(object sender, EventArgs e)
        {
            DataTransferenciaStock data = new DataTransferenciaStock
            {
                CodBodegaDesde = "PROD1CAP",
                CodBodegaHasta = "PSJ1",
                DocNumOF = "1304",
                Lineas = new List<Linea>()
                {
                    new Linea()
                    {
                        CodArticulo="11000064",
                        Lotes=new List<Lote>()
                        {
                            new Lote()
                            {
                                NombreLote="JB-221125150335",
                                Cantidad="36.93"
                            }

                        }
                    }
                }
            };
            ResponseDataTransferenciaStock response = new ResponseDataTransferenciaStock();
            Reply reply = new Reply();
            reply = await apiController.PostTransferenciaStock<ResponseDataTransferenciaStock>(data);
            response = (ResponseDataTransferenciaStock)reply.Data;
            MessageBox.Show(reply.StatusCode);
            listBox1.Items.Clear();
            listBox1.Items.Add(response.Id + " " + response.DocNum + " " + response.Error);

        }

        private async void btnCantidadPesada_Click(object sender, EventArgs e)
        {
            DataCantidadPesada data = new DataCantidadPesada
            {
                IdOf = "41648",
                CodArticulo = "11000084",
                CantPesada = "48"
            };
            ResponseCantidadPesada response = new ResponseCantidadPesada();
            Reply reply = new Reply();
            reply = await apiController.PostCantidadPesada<ResponseCantidadPesada>(data);
            response = (ResponseCantidadPesada)reply.Data;
            MessageBox.Show(reply.StatusCode);
            listBox1.Items.Clear();
            listBox1.Items.Add(response.Ms + " " + response.Error);
        }

        private async void btnLogin_ClickAsync(object sender, EventArgs e)
        {
            Reply reply = new Reply();
            //string data = @"{""User"":""Prueba"",""Pwd"":""prueba"",""AppName"":""Sistema de Balanzas""}";
            var usuarioJson = new JObject(
                new JProperty("User", txtNombre.Text),
                new JProperty("Pwd", txtContraseña.Text),
                new JProperty("AppName", "Sistema de Balanzas")
                );
            reply = await apiController.PostLoginUsuario(usuarioJson);
            string jsonResponse = (String)reply.Data;
            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            MessageBox.Show(reply.StatusCode);
            listBox1.Items.Clear();
            //listBox1.Items.Add(jsonData.Nombre+" " + jsonData.GruposDirectorioActivo[1]);
            foreach (String rol in jsonData.GruposDirectorioActivo)
            {
                listBox1.Items.Add(rol);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}