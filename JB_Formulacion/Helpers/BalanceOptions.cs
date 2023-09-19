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
        public BalanceOptions()
        {
            apiController = new ApiController();
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

                respuesta = "OK;" + contador + ";" + "vacío;" + orden.NumOrdenFabricacion + ";" + orden.Descripcion + ";" +
                    "vacío;" + "vacío;" + orden.BodegaDesde + ";" + orden.BodegaHasta + ";";

                return respuesta;
            }


        }
        public async Task<OrdenComponentes> DevolverMPsPorOF(string of)
        {
            OrdenComponentes orden = new OrdenComponentes();
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesConComponentes<OrdenComponentes>(of);

            orden = (OrdenComponentes)reply.Data;

            return orden;

        }

        public async Task<string>DevolverMPActual(string NumMateria,string of)
        {
            OrdenComponentes orden = await DevolverMPsPorOF(of);
            List<Componente> componentesPendientes=new List<Componente>();
            List<Componente> componentesPesados = new List<Componente>();

            foreach(Componente componente in orden.Componentes)
            {
                if(componente.CantidadPesada==0 || componente.CantidadPesada<componente.CantidadTotal)
                {
                    componentesPendientes.Add(componente);
                }
                else
                {
                    componentesPesados.Add(componente);
                }
            }

            return null;
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
        public Balanza CategorizarPeso(Componente componente)
        {
            Balanza balanza=new Balanza();
            if(componente.CantidadTotal>Settings1.Default.Bal1_Pmin && componente.CantidadTotal<Settings1.Default.Bal1_Pmax)
            {
                balanza.NumeroBalanza = 1;
                balanza.PesoMinimo = Settings1.Default.Bal1_Pmin;
                balanza.PesoMaximo = Settings1.Default.Bal1_Pmax;
                balanza.ToleranciaMinima = Settings1.Default.Bal1_Tolmin;
                balanza.ToleranciaMaxima = Settings1.Default.Bal1_Tolmax;
            }
            else if(componente.CantidadTotal>Settings1.Default.Bal2_Pmin && componente.CantidadTotal<Settings1.Default.Bal2_Pmax)
            {
                balanza.NumeroBalanza = 2;
                balanza.PesoMinimo = Settings1.Default.Bal2_Pmin;
                balanza.PesoMaximo=Settings1.Default.Bal2_Pmax;
                balanza.ToleranciaMinima=Settings1.Default.Bal2_Tolmin;
                balanza.ToleranciaMaxima = Settings1.Default.Bal2_Tolmax;
            }
            else if(componente.CantidadTotal>Settings1.Default.Bal3_Pmin && componente.CantidadTotal<Settings1.Default.Bal3_Pmax)
            {
                balanza.NumeroBalanza = 3;
                balanza.PesoMinimo = Settings1.Default.Bal3_Pmin;
                balanza.PesoMaximo=Settings1.Default.Bal3_Pmax;
                balanza.ToleranciaMinima = Settings1.Default.Bal3_Tolmin;
                balanza.ToleranciaMaxima=Settings1.Default.Bal3_Tolmax;
            }
            else if(componente.CantidadTotal>Settings1.Default.Bal4_Pmin && componente.CantidadTotal<Settings1.Default.Bal3_Pmax)
            {
                balanza.NumeroBalanza = 4;
                balanza.PesoMinimo=Settings1.Default.Bal4_Pmin;
                balanza.PesoMaximo = Settings1.Default.Bal4_Pmax;
                balanza.ToleranciaMinima = Settings1.Default.Bal4_Tolmin;
                balanza.ToleranciaMaxima=Settings1.Default.Bal4_Tolmax;
            }

            return balanza;
        }

        public void pasarDatosBalanzas(OrdenComponentes ordenComponentes)
        {
            OrdenComponentesBalanzas ordenBalanzas=new OrdenComponentesBalanzas();
            List<ComponenteBalanzas> componentesBalanzas=new List<ComponenteBalanzas>();
            ordenBalanzas.IdOf=ordenComponentes.IdOf;
            ordenBalanzas.NumOrdenFabricacion = ordenComponentes.NumOrdenFabricacion;
            ordenBalanzas.CodArticulo = ordenComponentes.CodArticulo;
            ordenBalanzas.Descripcion = ordenComponentes.Descripcion;
            ordenBalanzas.BodegaDesde = ordenComponentes.BodegaDesde;
            ordenBalanzas.BodegaHasta = ordenComponentes.BodegaHasta;
            foreach(Componente componente in ordenComponentes.Componentes)
            {
                ComponenteBalanzas componenteBalanzas=new ComponenteBalanzas();
                componenteBalanzas.CodigoArticulo = componente.CodigoArticulo;
                componenteBalanzas.UnidadMedida = componente.UnidadMedida;
                componenteBalanzas.Descripcion=componente.Descripcion;
            }
        }






    }
}
