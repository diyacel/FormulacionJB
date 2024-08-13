using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecepciónPesosJamesBrown.Helpers;
using RecepciónPesosJamesBrown.Models.Api;
using RecepciónPesosJamesBrown.Models.DAO;

namespace RecepciónPesosJamesBrown
{
    public partial class Form2 : Form
    {
        BalanceOptions options;
        ApplicationDbContext context;
        MapperConfiguration mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile(new MappingProfile());
        });
        public Form2()
        {
            InitializeComponent();
            context = new ApplicationDbContext();
            IMapper mapper = mapperConfig.CreateMapper();
            options = new BalanceOptions(context, mapper);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoteTranApi[] lotes = new LoteTranApi[2];
            lotes[0] = new LoteTranApi
            {
                Lote = "111",
                Cantidad = 45.90M

            };
            lotes[1] = new LoteTranApi
            {
                Lote = "222",
                Cantidad = 60.178M
            };
            if (GuardarLineaEnTransferenciaStock("30015134", "11000291", lotes) > 0)
                MessageBox.Show("se registraron");
            else
            {
                MessageBox.Show("no se registraron");
            }
        }
        public int GuardarLineaEnTransferenciaStock(string orden, string articulo, LoteTranApi[] lotes)
        {
            int numCambios = 0;
            var transferencia = context.Transferencias.Include(t => t.lineas)
                .FirstOrDefault(t => t.NumOrdenFabricacion == int.Parse(orden));

            if (transferencia != null)
            {
                var nuevaLinea = new LineaDAO
                {
                    NumOrdenFabricacion = int.Parse(orden),
                    CodArticulo = articulo

                };
                if (lotes.Count() > 1)
                {
                    nuevaLinea.Lotes = new List<LoteDAO>
                            {
                                new LoteDAO {  NumOrdenFabricacion=int.Parse(orden),
                                    CodArticulo=articulo, Lote = lotes[0].Lote, Cantidad =lotes[0].Cantidad
                                },
                                new LoteDAO { NumOrdenFabricacion=int.Parse(orden),
                                    CodArticulo=articulo, Lote = lotes[1].Lote, Cantidad = lotes[1].Cantidad,
                                }
                            };
                }
                else
                {
                    nuevaLinea.Lotes = new List<LoteDAO>
                            {
                                new LoteDAO { NumOrdenFabricacion=int.Parse(orden),
                                   CodArticulo=articulo, Lote = lotes[0].Lote, Cantidad =lotes[0].Cantidad }
                            };
                }
                transferencia.lineas.Add(nuevaLinea);
                context.Lineas.Add(nuevaLinea);
            }
            numCambios = context.SaveChanges();
            return numCambios;

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = await options.EnviarTransferenciaStock("30015130");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string json = await options.DevolverOFJSON("30015228");
            textBox1.Text = json;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
