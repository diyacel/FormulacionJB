using JB_Formulacion.Controllers;
using JB_Formulacion.Helper;
using JB_Formulacion.Models;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JB_Formulacion.Helpers
{
    public class BalanceOptions
    {

        ApiController apiController;
        OrdenComponentes ordenActualComponentes;
        public BalanceOptions()
        {
            apiController = new ApiController();
            ordenActualComponentes = new OrdenComponentes();
        }

        /// <summary>
        /// OF -> Orden de Fabricación Liberada
        /// </summary>
        public async Task<string> DevolverOFs()
        {
            List<OrdenFabricacion> ordenes = new List<OrdenFabricacion>();
            List<string> numerosOrdenes = new List<string>();
            string linea_numeroOrdenenes = string.Empty;
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesFabricacion<List<OrdenFabricacion>>();
            ordenes = (List<OrdenFabricacion>)reply.Data;
            foreach (var orden in ordenes)
            {
                numerosOrdenes.Add(orden.NumOrdenFabricacion.ToString());
                linea_numeroOrdenenes = linea_numeroOrdenenes + orden.NumOrdenFabricacion.ToString() +
                    "-"+orden.Descripcion.Substring(1,15)+",";
            }

            return linea_numeroOrdenenes;

        }
        public async Task<List<string>> DevolverOFsPorArticulo(string articulo)
        {
            List<OrdenFabricacion> ordenes = new List<OrdenFabricacion>();
            List<string> numerosOrdenes = new List<string>();
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesFabricacion<List<OrdenFabricacion>>();
            ordenes = (List<OrdenFabricacion>)reply.Data;
            foreach (var orden in ordenes)
            {
                if (orden.CodigoArticulo.Equals(articulo))
                {
                    numerosOrdenes.Add(orden.NumOrdenFabricacion.ToString());
                }

            }

            return numerosOrdenes;
        }
        public async Task<string> DevolverCantidadMPsPorOF(string of)
        {
            OrdenComponentes orden = new OrdenComponentes();
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesConComponentes<OrdenComponentes>(of);
            if (reply.StatusCode.Equals("error"))
            {
                int intValue = 21;
                return char.ConvertFromUtf32(intValue) + reply.Data.ToString();
            }
            else
            {
                orden = (OrdenComponentes)reply.Data;
                string respuesta = string.Empty;
                int contador = 0;
                foreach (Componente componente in orden.Componentes)
                {
                    foreach (Lote lote in componente.CantidadesPorLote)
                    {
                        contador += 1;
                    }
                }

                respuesta = "OK;" + contador + ";" + " ;" + orden.NumOrdenFabricacion + ";" + orden.Descripcion + ";" +
                    " ;" + " ;" + orden.BodegaDesde + ";" + orden.BodegaHasta + ";";

                return respuesta;
            }


        }
        public async Task CargarMPsPorOF(string of)
        {
            OrdenComponentes orden = new OrdenComponentes();
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesConComponentes<OrdenComponentes>(of);

            orden = (OrdenComponentes)reply.Data;

            ordenActualComponentes = orden;
            

        }

        public String DevolverMPActual(string numComponente,string of)
        {
            OrdenComponentes orden = DevolverOrdenBalanzas(of);
            ComponenteBalanzas comp = new ComponenteBalanzas();
            int numeroComponentes=orden.Componentes.Count();
            string respuesta = string.Empty;
            try
            {
                int numero = int.Parse(numComponente);
                for (int i = 0; i < numero; i++)
                {
                    comp = (ComponenteBalanzas)orden.Componentes[i];
                }
                respuesta = comp.Descripcion + ";" + comp.CantidadLote + ";" + comp.Balanza.NumeroBalanza + ";" +
                        comp.CodigoArticulo + ";" + comp.UnidadMedida + ";" + comp.Balanza.ToleranciaMinima + ";" +
                        comp.Balanza.ToleranciaMaxima + ";" + comp.NombreLote;
            }catch (Exception ex)
            {
                int intValue = 21;
                respuesta= char.ConvertFromUtf32(intValue) + ex.Message;
            }
            

            return respuesta;
            

        }
        public async Task<string> DevolverGruposDirectorioActivo(string usuario, string contraseña)
        {
            List<string> roles= new List<string>();
            Reply reply = new Reply();
            string respuesta = string.Empty;
            var usuarioJson = new JObject(
                new JProperty("User", usuario),
                new JProperty("Pwd", contraseña),
                new JProperty("AppName", "Sistema de Balanzas")
             );
            reply = await apiController.PostLoginUsuario(usuarioJson);
            string jsonResponse = (String)reply.Data;
            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            foreach (String rol in jsonData.GruposDirectorioActivo)
            {
                roles.Add(rol);
            }
            
            if (roles.Any())
            {
                foreach (string rol in roles)
                {
                    if (rol.Equals("tics") || rol.Equals("bodega"))
                    {
                        respuesta = "OK;";
                    }
                    if (rol.Equals("Usuarios del dominio"))
                    {
                        int intValue = 21;
                        respuesta = char.ConvertFromUtf32(intValue);
                        respuesta += "Usuario no autorizado";
                    }
                }
            }
            else
            {
                int intValue = 21;
                respuesta = char.ConvertFromUtf32(intValue);
                respuesta += "Usuario o contraseña inválidos";
            }

            return respuesta;


        }

        public bool EsPesoValido(Componente componente)
        {
            int numeroLotes=componente.CantidadesPorLote.Count;
            return false;
        }
        public Balanza EscogerBalanza(Componente componente)
        {
            Balanza balanza = new Balanza();
            double cantidadConvertida = 0;
            switch (componente.UnidadMedida)
            {
                case "g":
                    cantidadConvertida = UnitConverter.ConvertGToKg(componente.CantidadTotal); 
                    break;
                case "mg":
                    cantidadConvertida=UnitConverter.ConvertMgToKg(componente.CantidadTotal);
                    break;
                case "kg":
                    cantidadConvertida = componente.CantidadTotal;
                    break;
            }
            
            if (cantidadConvertida > Settings1.Default.Bal1_Pmin && cantidadConvertida < Settings1.Default.Bal1_Pmax)
            {
                balanza.NumeroBalanza = 1;
                balanza.PesoMinimo = Settings1.Default.Bal1_Pmin;
                balanza.PesoMaximo = Settings1.Default.Bal1_Pmax;
                balanza.ToleranciaMinima = Settings1.Default.Bal1_Tolmin;
                balanza.ToleranciaMaxima = Settings1.Default.Bal1_Tolmax;
            }
            else if(cantidadConvertida>Settings1.Default.Bal2_Pmin && cantidadConvertida<Settings1.Default.Bal2_Pmax)
            {
                balanza.NumeroBalanza = 2;
                balanza.PesoMinimo = Settings1.Default.Bal2_Pmin;
                balanza.PesoMaximo=Settings1.Default.Bal2_Pmax;
                balanza.ToleranciaMinima=Settings1.Default.Bal2_Tolmin;
                balanza.ToleranciaMaxima = Settings1.Default.Bal2_Tolmax;
            }
            else if(cantidadConvertida>Settings1.Default.Bal3_Pmin && cantidadConvertida<Settings1.Default.Bal3_Pmax)
            {
                balanza.NumeroBalanza = 3;
                balanza.PesoMinimo = Settings1.Default.Bal3_Pmin;
                balanza.PesoMaximo=Settings1.Default.Bal3_Pmax;
                balanza.ToleranciaMinima = Settings1.Default.Bal3_Tolmin;
                balanza.ToleranciaMaxima=Settings1.Default.Bal3_Tolmax;
            }
            else if(cantidadConvertida>Settings1.Default.Bal4_Pmin && cantidadConvertida<Settings1.Default.Bal3_Pmax)
            {
                balanza.NumeroBalanza = 4;
                balanza.PesoMinimo=Settings1.Default.Bal4_Pmin;
                balanza.PesoMaximo = Settings1.Default.Bal4_Pmax;
                balanza.ToleranciaMinima = Settings1.Default.Bal4_Tolmin;
                balanza.ToleranciaMaxima=Settings1.Default.Bal4_Tolmax;
            }

            return balanza;
        }

        public OrdenComponentes DevolverOrdenBalanzas(string of)
        {
            //Recibir el componente con el número de OF
            OrdenComponentes orden =ordenActualComponentes;
            //Crear la orden balanzas con sus campos
            OrdenComponentes ordenBalanzas = new OrdenComponentes();
            ordenBalanzas.NumOrdenFabricacion = orden.NumOrdenFabricacion;
            ordenBalanzas.IdOf = orden.IdOf;
            ordenBalanzas.Descripcion = orden.Descripcion;
            ordenBalanzas.BodegaDesde = orden.BodegaDesde;
            ordenBalanzas.BodegaHasta = orden.BodegaHasta;


            //Asignar los componentes balanzas y lote balanzas
            foreach (Componente componente in orden.Componentes)
            {
                string res = CategorizarPeso(componente);
                MessageBox.Show(res);
                if (componente.CantidadPesada == 0)
                {
                   
                    if (componente.CantidadesPorLote is not null)
                    {
                        foreach (Lote lote in componente.CantidadesPorLote)
                        {
                            ComponenteBalanzas componenteBalanzas = new ComponenteBalanzas();
                            componenteBalanzas.UnidadMedida = componente.UnidadMedida;
                            componenteBalanzas.Descripcion = componente.Descripcion;
                            componenteBalanzas.CodigoArticulo = componente.CodigoArticulo;
                            componenteBalanzas.Balanza = EscogerBalanza(componente);
                            componenteBalanzas.NombreLote = lote.NombreLote;
                            componenteBalanzas.CantidadLote = lote.Cantidad;
                            componenteBalanzas.CantidadPesadaLote = 0;
                            ordenBalanzas.Componentes.Add(componenteBalanzas);
                        }

                    }

                    

                }
                else
                {
                    int numLotes=componente.CantidadesPorLote.Count();
                    Balanza balanza = EscogerBalanza(componente);

                    
                    

                }
            }

            //Ordenar los componentes balanza según la numeración del código de artículo
            List<Componente> componentesOrdenados = ordenBalanzas.Componentes.
                OrderBy(componente => componente.CodigoArticulo).ToList();
            ordenBalanzas.Componentes = componentesOrdenados;

            return ordenBalanzas;
        }

        public string CategorizarPeso(Componente componente)
        {
            int numeroLotes = componente.CantidadesPorLote.Count();
            Balanza balanza= EscogerBalanza(componente);
            double valMax = Math.Round(numeroLotes * componente.CantidadTotal+UnitConverter.ConvertGToKg(balanza.ToleranciaMaxima),2);
            double valMin = Math.Round(numeroLotes * componente.CantidadTotal-UnitConverter.ConvertGToKg(balanza.ToleranciaMinima),2);
            if (componente.CantidadPesada<=valMax && componente.CantidadPesada>=valMin)
            {
                return "peso en rango";
                //estado: pesado
                // repartir cantidadPesada entre los pesos de cada lote
            }
            else if(componente.CantidadPesada!=0 && componente.CantidadPesada<valMin && numeroLotes>1 )
            {
                //cantidad del lote 1
                //cantidad del lote 2
                //restar cantidad pesada - lote
                //estado: pendiente

                foreach (Lote lote in componente.CantidadesPorLote)
                {
                    double diferenciaActual = Math.Abs(componente.CantidadPesada - lote.Cantidad);
                    double diferenciaMasCercana = Math.Abs(valorRecibido - valorMasCercano);

                    if (diferenciaActual < diferenciaMasCercana)
                        valorMasCercano = valor;
                }


                return "algo ya se peso";
              
            }
            else if(componente.CantidadPesada==0)
            {
                //pesos de cada lote =0
                //estado: pendiente
                return "nada pesado";
            }
            else
            {
                return "";
            }
        }








    }
}
