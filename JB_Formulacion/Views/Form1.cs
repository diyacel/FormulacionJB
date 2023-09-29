using Azure;
using JB_Formulacion.Controllers;
using JB_Formulacion.Helper;
using JB_Formulacion.Helpers;
using JB_Formulacion.Models;
using Microsoft.EntityFrameworkCore;
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
            //List<OrdenFabricacion> ordenes = new List<OrdenFabricacion>();
            //Reply reply = new Reply();
            //reply = await apiController.GetOrdenesFabricacion<List<OrdenFabricacion>>();
            //ordenes = (List<OrdenFabricacion>)reply.Data;
            //MessageBox.Show(reply.StatusCode);

            //listBox1.Items.Clear();
            //foreach (OrdenFabricacion orden in ordenes)
            //{
            //    listBox1.Items.Add(orden.NumOrdenFabricacion.ToString());
            //}

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
                foreach (CantidadPorLote lote in componente.CantidadesPorLote)
                {
                    listBox1.Items.Add(lote.Lote.ToString() + " " + lote.Cantidad.ToString());
                }
                listBox1.Items.Add(componente.CantidadPesada.ToString());
                balanza = options.EscogerBalanza(componente);
                listBox1.Items.Add("Numero de balanza: " + balanza.NumeroBalanza.ToString());
                listBox1.Items.Add("Numero de lotes: " + componente.CantidadesPorLote.Count);


            }
        }


        private async void btnStock_ClickAsync(object sender, EventArgs e)
        {
            //DataTransferenciaStock data = new DataTransferenciaStock
            //{
            //    CodBodegaDesde = "PROD1CAP",
            //    CodBodegaHasta = "PSJ1",
            //    DocNumOF = "1304",
            //    Lineas = new List<Linea>()
            //    {
            //        new Linea()
            //        {
            //            CodArticulo="11000064",
            //            Lotes=new List<CantidadPorLote>()
            //            {
            //                new CantidadPorLote()
            //                {
            //                    Lote="JB-221125150335",
            //                    Cantidad=36.93
            //                }

            //            }
            //        }
            //    }
            //};
            //ResponseDataTransferenciaStock response = new ResponseDataTransferenciaStock();
            //Reply reply = new Reply();
            //reply = await apiController.PostTransferenciaStock<ResponseDataTransferenciaStock>(data);
            //response = (ResponseDataTransferenciaStock)reply.Data;
            //MessageBox.Show(reply.StatusCode);
            //listBox1.Items.Clear();
            //listBox1.Items.Add(response.Id + " " + response.DocNum + " " + response.Error);

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

        private async void button1_Click(object sender, EventArgs e)
        {
            //OrdenComponentes orden = new OrdenComponentes();
            //Reply reply = new Reply();
            //reply = await apiController.GetOrdenesConComponentes<OrdenComponentes>(txtNumeroOrden.Text);
            //orden = (OrdenComponentes)reply.Data;
            //MessageBox.Show(reply.StatusCode);
            //Balanza balanza = new Balanza();

            await options.CargarMPsPorOF(txtNumeroOrden.Text);
            OrdenComponentes ordenBalanzas = options.DevolverOrdenBalanzas(txtNumeroOrden.Text);



            listBox1.Items.Clear();
            foreach (ComponenteBalanzas componente in ordenBalanzas.Componentes)
            {
                listBox1.Items.Add("ARTICULO: " + componente.CodigoArticulo.ToString() + " UNIDAD: " + componente.UnidadMedida + " DESCRIPCIÓN " + componente.Descripcion.ToString());
                listBox1.Items.Add("BALANZA: " + componente.Balanza.NumeroBalanza.ToString());
                listBox1.Items.Add("LOTE: " + componente.NombreLote + " CANTIDAD: " + componente.CantidadLote + " PESADA: " + componente.CantidadPesadaLote);
                //foreach (LoteBalanzas lote in componente.CantidadesPorLote)
                //{
                //    listBox1.Items.Add("NOMBRE:" + lote.Lote.ToString() + " CANTIDAD:" + lote.Cantidad.ToString() + " CANTIDAD PESADA: " + lote.CantidadPesada.ToString());
                //}
                // listBox1.Items.Add("NUMERO DE LOTES: " + componente.CantidadesPorLote.Count);


            }
        }

        private async void btnNumeroComponente_Click(object sender, EventArgs e)
        {
            await options.CargarMPsPorOF("10009132");
            string respuesta = options.DevolverMPActual(txtNumComponente.Text, "10009132");
            MessageBox.Show(respuesta);
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            OrdenComponentes orden = new OrdenComponentes();
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesConComponentes<OrdenComponentes>(txtNumeroOrden.Text);
            orden = (OrdenComponentes)reply.Data;
            MessageBox.Show(reply.StatusCode);

            using (var context = new ApplicationDbContext())
            {
                // context.Ordenes.Add(orden);

                await context.SaveChangesAsync();
                MessageBox.Show("guardado exitoso!");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //OrdenComponentes orden = new OrdenComponentes();
            //using(var context = new ApplicationDbContext()) 
            //{
            //    //orden = context.Ordenes
            //    //.Include(o => o.Componentes.Select(c => (c as 'Lotes').CantidadPorLote)
            //    //.FirstOrDefault(o => o.IdOf == 41648);

            //    //var ordenConComponentesYCantidadesLotes = context.Ordenes
            //    //.Include(o => o.Componentes.Select(c => c.CantidadesPorLote))
            //    //.FirstOrDefault(o => o.IdOf == 41648);

            //    orden = context.Ordenes
            //    .Include(o => o.Componentes)
            //    .ThenInclude(l => l.CantidadesPorLote)
            //    .FirstOrDefault(o => o.IdOf == 41648);
            //}

            //listBox1.Items.Clear();
            ////listBox1.Items.Add(orden.Componentes)
            //foreach (Componente componente in orden.Componentes)
            //{
            //    listBox1.Items.Add(componente.CodigoArticulo.ToString() + " UNIDAD: " + componente.UnidadMedida + " " + componente.Descripcion.ToString());
            //    foreach (CantidadPorLote lote in componente.CantidadesPorLote)
            //    {
            //        listBox1.Items.Add(lote.Lote.ToString() + " " + lote.Cantidad.ToString());
            //    }
            //    listBox1.Items.Add(componente.CantidadPesada.ToString());
            //   // balanza = options.EscogerBalanza(componente);
            //   // listBox1.Items.Add("Numero de balanza: " + balanza.NumeroBalanza.ToString());
            //    listBox1.Items.Add("Numero de lotes: " + componente.CantidadesPorLote.Count);


            //}

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string respuesta = await options.DevolverOFs();
            string[] ordenes = respuesta.Split(',');
            foreach (string str in ordenes)
            {
                listBox1.Items.Add(str);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await DevoLverComponentes(txtNumeroOrden.Text);
        }

        public async Task DevoLverComponentes(string idof)
        {
            try
            {
                List<string> roles = new List<string>();
                Reply reply = new Reply();
                string json = await apiController.ObtenerOrdenes(idof);
                OrdenFabricacion orden = new OrdenFabricacion();
                MateriaPrima materia = new MateriaPrima();
                Transferencia transferencia = new Transferencia();
                CantidadPorLote lote = new CantidadPorLote();

                dynamic jsonData = JsonConvert.DeserializeObject(json);
                listBox1.Items.Add("NumeroOrdenFabricacion:" + jsonData.NumOrdenFabricacion.ToString());
                orden.NumOrdenFabricacion = jsonData.NumOrdenFabricacion;
                orden.CodigoArticulo = jsonData.CodArticulo;
                orden.Descripcion = jsonData.Descripcion;
                transferencia.DocNumOf = jsonData.IdOf;
                transferencia.CodBodegaDesde = jsonData.BodegaDesde;
                transferencia.CodBodegaHasta = jsonData.BodegaHasta;
                transferencia.Estado = "Pendiente";
                orden.CantidadesPorLote = new List<CantidadPorLote>();
                transferencia.CantidadesPorLote = new List<CantidadPorLote>();
                using (var context = new ApplicationDbContext())
                {
                    foreach (dynamic item in jsonData.Componentes)
                    {
                        listBox1.Items.Add("CodigoArticulo:" + item.CodigoArticulo);
                        materia = new MateriaPrima();
                        materia.Codigo = item.CodigoArticulo;
                        materia.UnidadMedida = item.UnidadMedida;
                        materia.Descripcion = item.Descripcion;
                        materia.CantidadesPorLote = new List<CantidadPorLote>();
                        foreach (dynamic i in item.CantidadesPorLote)
                        {
                            listBox1.Items.Add("Lote: " + i.Lote);
                            lote = new CantidadPorLote();
                            lote.Lote = i.Lote;
                            lote.Cantidad = i.Cantidad;
                            lote.CantidadPesada = 0;
                            lote.CantidadTotal = item.CantidadTotal;
                            orden.CantidadesPorLote.Add(lote);

                            materia.CantidadesPorLote.Add(lote);

                            transferencia.CantidadesPorLote.Add(lote);

                            context.Lotes.Add(lote);


                        }
                        context.Materias.Add(materia);
                    }

                    context.Ordenes.Add(orden);
                    context.Transferencias.Add(transferencia);

                    await context.SaveChangesAsync();
                    MessageBox.Show("guardado exitoso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void button5_Click(object sender, EventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                Transferencia transferencia = context.Transferencias.Where(s => s.DocNumOf == int.Parse(txtNumeroOrden.Text)).FirstOrDefault();
                List<CantidadPorLote> lotes = await context.Lotes.Where(s => s.Transferencia.DocNumOf == int.Parse(txtNumeroOrden.Text))
                    .Include(s => s.MateriaPrima)
                    .ToListAsync();

                foreach (CantidadPorLote lote in lotes)
                {
                    listBox1.Items.Add(lote.MateriaPrima.Codigo);
                    listBox1.Items.Add(lote.Lote);
                }
                listBox1.Items.Add(lotes.Count);

                List<MateriaPrima> materias= await context.Materias.Where(s=>s.CantidadesPorLote)
            }
        }
    }
}