using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecepciónPesosJamesBrown.Controllers;
using RecepciónPesosJamesBrown.Models;
using RecepciónPesosJamesBrown.Models.Api;
using RecepciónPesosJamesBrown.Models.DAO;
using StackExchange.Redis;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace RecepciónPesosJamesBrown.Helpers
{
    public class BalanceOptions
    {

        ApiController apiController;
        OrdenComponentesApi ordenComponentesApiActual;
        List<ComponenteApi> componentesProductoPorCampaña;
        List<ComponenteApi> componentesProductoPorOrden;
        List<string> NumOrdenesCampaña;
        decimal lotePesadoTemporalPO;
        decimal lotePesadoTemporalProductoPorCampaña;
        private readonly ApplicationDbContext context;
        private readonly IMapper _mapper;
        private readonly MemoryCache _cache;
        private bool _seTerminoCargarCache;
        public BalanceOptions(ApplicationDbContext context, IMapper mapper)
        {
            _seTerminoCargarCache = false;
            this.context = context;
            this._mapper = mapper;
            apiController = new ApiController();
            lotePesadoTemporalPO = 0;
            lotePesadoTemporalProductoPorCampaña = 0;
            ordenComponentesApiActual = new OrdenComponentesApi();
            componentesProductoPorCampaña = new List<ComponenteApi>();
            componentesProductoPorOrden = new List<ComponenteApi>();
            NumOrdenesCampaña = new List<string>();
            _cache = new MemoryCache(new MemoryCacheOptions());
            RegistrartrarOrdenesCache2();
        }
        public BalanceOptions()
        {

        }

        public async Task<string> CargarProductoPorOrden(string numeroOrden)
        {
            Reply reply=new Reply();
            OrdenComponentesApi orden=await DevolverOrdenComponentes(numeroOrden);
            if (orden is not null)
            {

                componentesProductoPorOrden = CompletarComponentes(orden);

                lotePesadoTemporalPO = 0;

                RevisarTransferenciaDeStock(orden);

                if (componentesProductoPorOrden is not null)
                {
                    if (ContarComponentesCantidadPesadaCero(componentesProductoPorOrden) > 0)
                    {
                        ComponenteApi primerComponente = componentesProductoPorOrden.FirstOrDefault(c => c.CantidadPesada == 0);
                        return DevolverLote(primerComponente);
                    }
                    else
                    {
                        return await ValidarOrdenTerminadaPO(componentesProductoPorOrden);

                    }
                }
                else
                {
                    int intValue = 21;
                    string respuesta = char.ConvertFromUtf32(intValue);
                    respuesta += "La orden: " + numeroOrden + " no tiene componentes asignados";
                    return respuesta;
                }

            }
            else
            {
                  int intValue = 21;
                  string respuesta = char.ConvertFromUtf32(intValue);
                  respuesta += "No existe el numero de orden: " + numeroOrden;
                  return respuesta;
            }


        }
        public async Task<string> CargarProductoPorCampaña(string codigoArticulo)
        {
            try
            {
                string respuesta = string.Empty;
                List<string> numeroOrdenes = await DevolverOFsPorMP(codigoArticulo);
                NumOrdenesCampaña = numeroOrdenes;
                ordenComponentesApiActual = new OrdenComponentesApi();
                componentesProductoPorCampaña = new List<ComponenteApi>();
                foreach (string num in numeroOrdenes)
                {
                    //OrdenComponentesApi orden = await DevolverOrdenComponentes(num);
                    //var redisDB = RedisDB.Connection.GetDatabase();
                    //string ordenJSON = redisDB.StringGet(num);
                    _cache.TryGetValue(num, out string ordenJSON);

                    //RevisarTransferenciaDeStock(orden);
                    if (ordenJSON is not null)
                    {
                        OrdenComponentesApi orden = JsonConvert.DeserializeObject<OrdenComponentesApi>(ordenJSON);
                        if (orden.Componentes is not null)
                        {
                            foreach (ComponenteApi comp in orden.Componentes)
                            {
                                if (comp.CodigoArticulo.Equals(codigoArticulo) && comp.CantidadPesada == 0)
                                {
                                    comp.NumOrdenFabricacion = orden.NumOrdenFabricacion;
                                    comp.IdOf = orden.IdOf;
                                    comp.LotePT = orden.LotePT;
                                    comp.DescripcionProducto = orden.Descripcion;
                                    componentesProductoPorCampaña.Add(comp);
                                }
                            }
                        }

                    }

                }
                ComponenteApi primerComponente = new ComponenteApi();
                primerComponente = componentesProductoPorCampaña.FirstOrDefault(c => c.CantidadPesada == 0);
                if (componentesProductoPorCampaña.Count() == 0)
                {
                    return "";
                }
                else
                {
                    return DevolverLote(primerComponente);
                }

            }
            catch (Exception ex)
            {
                int intValue = 21;
                string respuesta = char.ConvertFromUtf32(intValue);
                respuesta += "No se encuentra el código de articulo ingresado";
                return respuesta;
            }




        }
        public async Task<string> CargarProductoPorCampaña2(string codigoArticulo)
        {

            string respuesta = string.Empty;
            NumOrdenesCampaña= await DevolverOFsPorMP(codigoArticulo);

            foreach(string num in NumOrdenesCampaña)
            {
                _cache.TryGetValue(num, out string ordenJSON);
                if (ordenJSON != null)
                {
                    OrdenComponentesApi orden = JsonConvert.DeserializeObject<OrdenComponentesApi>(ordenJSON);
                    if(orden.Componentes!= null)
                    {
                        foreach(ComponenteApi componente in orden.Componentes)
                        {
                            if(componente.CodigoArticulo.Equals(codigoArticulo) && componente.CantidadPesada==0)
                            {
                                return DevolverLote(componente);
                            }
                        }
                    }
                }
            }


        }
        public async Task<string> SaltarComponente2(string numeroOrden, string codigoArticulo, string tipoProducto)
        {
            string respuesta = string.Empty;
            ComponenteApi componenteSiguiente = ObtenerSiguienteComponente3(numeroOrden, codigoArticulo,tipoProducto);
            if (tipoProducto.Equals("Orden"))
            {

                if (componenteSiguiente != null)
                {
                    return DevolverLote(componenteSiguiente);
                }
                else
                {
                    return await ValidarOrdenTerminadaPO(componentesProductoPorOrden);
                }
            }
            else 
            {
                if (componenteSiguiente != null)
                {
                    return DevolverLote(componenteSiguiente);
                }
                else
                {
                    return await ValidarOrdenTerminadaPO(componentesProductoPorOrden);
                }
            }
        }
        public ComponenteApi ObtenerSiguienteComponente2(string numeroOrden, string codArticulo, string tipoProducto)
        {
            if (tipoProducto.Equals("Orden"))
            {
                for (int i = 0; i < componentesProductoPorOrden.Count; i++)
                {
                    ComponenteApi componenteActual = componentesProductoPorOrden[i];

                    // Verifica si el componente actual cumple con las condiciones
                    if (componenteActual.CodigoArticulo.Equals(codArticulo) && componenteActual.CantidadPesada == 0)
                    {
                        // Devuelve el siguiente componente o el primer componente si es el último
                        return componentesProductoPorOrden[(i + 1) % componentesProductoPorOrden.Count];
                    }
 
                }

            }
            else
            {
                for (int i = 0; i < componentesProductoPorCampaña.Count; i++)
                {
                    ComponenteApi componenteActual = componentesProductoPorCampaña[i];

                    // Verifica si el componente actual cumple con las condiciones
                    if (componenteActual.NumOrdenFabricacion.Equals(numeroOrden) && componenteActual.CantidadPesada == 0)
                    {
                        // Devuelve el siguiente componente o el primer componente si es el último
                        return componentesProductoPorCampaña[(i + 1) % componentesProductoPorCampaña.Count];
                    }
                }
            }
            return null;
        }
        public ComponenteApi ObtenerSiguienteComponente3(string numeroOrden, string codArticulo, string tipoProducto)
        {
            if (tipoProducto.Equals("Orden"))
            {
                for (int i = 0; i < componentesProductoPorOrden.Count; i++)
                {
                    ComponenteApi componenteActual = componentesProductoPorOrden[i];

                    // Verifica si el componente actual cumple con las condiciones
                    if (componenteActual.CodigoArticulo.Equals(codArticulo) && componenteActual.CantidadPesada == 0)
                    {
                        // Devuelve el siguiente componente con peso cero o null si no hay ninguno
                        for (int j = (i + 1) % componentesProductoPorOrden.Count; j != i; j = (j + 1) % componentesProductoPorOrden.Count)
                        {
                            if (componentesProductoPorOrden[j].CantidadPesada == 0)
                            {
                                return componentesProductoPorOrden[j];
                            }
                        }
                        return null; // Si no se encuentra ningún componente con peso cero después de este
                    }
                }
            }
            return null; // Si no hay componentes para la orden especificada
        }
        public string DevolverSegundoLote2(string numeroOrden, string codigoArticulo, string peso,string tipoProducto)
        {
            lotePesadoTemporalPO = decimal.Parse(peso.Replace('.', ','));
            ComponenteApi componente = ObtenerComponente2(numeroOrden, codigoArticulo,tipoProducto);
            if (tipoProducto.Equals("Orden"))
            {
                LoteComponentesApi lote = componente.CantidadesPorLote.OrderByDescending(c => c.Cantidad).FirstOrDefault();
                Balanza balanza = EscogerBalanza(componente);
                decimal cantidadRequeridaNueva = componente.CantidadRequerida - decimal.Parse(peso.Replace('.', ','));
                return componente.Descripcion + ";" + cantidadRequeridaNueva.ToString().Replace(',', '.') + ";"
                            + balanza.NumeroBalanza + ";" + componente.CodigoArticulo + ";"
                            + componente.UnidadMedida + ";" + balanza.ToleranciaMinima + ";"
                            + balanza.ToleranciaMaxima + ";" + lote.Lote + ";" + 1 + ";"
                            + ordenComponentesApiActual.LotePT + ";" + lote.FechaVence + ";" + QuitarTildes(ObtenerCodigoAnalisis(lote.AnalisisMP)) + ";"
                            + ordenComponentesApiActual.NumOrdenFabricacion + ";" + RecortarCaracteres(componente.DescripcionProducto, 40) + ";";
            }
            else
            {
                LoteComponentesApi lote = componente.CantidadesPorLote.OrderByDescending(c => c.Cantidad).FirstOrDefault();
                Balanza balanza = EscogerBalanza(componente);
                decimal cantidadRequeridaNueva = componente.CantidadRequerida - decimal.Parse(peso.Replace('.', ','));
                return componente.Descripcion + ";" + cantidadRequeridaNueva.ToString().Replace(',', '.') + ";"
                            + balanza.NumeroBalanza + ";" + componente.CodigoArticulo + ";"
                            + componente.UnidadMedida + ";" + balanza.ToleranciaMinima + ";"
                            + balanza.ToleranciaMaxima + ";" + lote.Lote + ";" + 1 + ";"
                            + componente.LotePT + ";" + lote.FechaVence + ";" + QuitarTildes(ObtenerCodigoAnalisis(lote.AnalisisMP)) + ";"
                            + componente.NumOrdenFabricacion + ";" + RecortarCaracteres(componente.DescripcionProducto, 40) + ";";

            }




        }
        public async Task<string> GuardarComponentePesado2(string numeroOrden, string codigoArticulo, string peso, string tipoProducto)
        {
            try
            {
                string respuesta = string.Empty;
                ComponenteApi componente = ObtenerComponente2(numeroOrden, codigoArticulo, tipoProducto);
                if (componente.CantidadesPorLote.Count() > 1)
                {
                    decimal cantidadComponente = decimal.Parse(peso.Replace('.', ',')) + lotePesadoTemporalPO;
                    respuesta = await EnviarComponentePesado(componente.IdOf.ToString(), codigoArticulo, cantidadComponente.ToString());
                    if (respuesta.IsNullOrEmpty())
                    {
                        LoteTranApi[] lotes = new LoteTranApi[2];
                        lotes[0] = new LoteTranApi();
                        lotes[1] = new LoteTranApi();
                        lotes[0].Lote = componente.CantidadesPorLote.OrderBy(c => c.Cantidad).Select(l => l.Lote).FirstOrDefault();
                        lotes[1].Lote = componente.CantidadesPorLote.OrderByDescending(c => c.Cantidad).Select(l => l.Lote).FirstOrDefault();
                        lotes[0].Cantidad = lotePesadoTemporalPO;
                        lotes[1].Cantidad = decimal.Parse(peso.Replace('.', ','));

                        GuardarLineaEnTransferenciaStock(numeroOrden, codigoArticulo, lotes);

                        if(tipoProducto.Equals("Orden"))
                        {
                            foreach(ComponenteApi comp in componentesProductoPorOrden)
                            {
                                if(comp.NumOrdenFabricacion.Equals(numeroOrden) && comp.CodigoArticulo.Equals(codigoArticulo))
                                {
                                    comp.CantidadPesada = cantidadComponente;
                                }
                            }
                        }
                        else
                        {
                            foreach(ComponenteApi comp in componentesProductoPorCampaña)
                            {
                                if (comp.NumOrdenFabricacion.Equals(numeroOrden) && comp.CodigoArticulo.Equals(codigoArticulo))
                                {
                                    comp.CantidadPesada = cantidadComponente;
                                }
                            }
                        }

                        return await SaltarComponente2(numeroOrden, codigoArticulo, tipoProducto);
                    }
                    else
                    {
                        return QuitarTildes(respuesta);
                    }

                }
                else
                {
                    respuesta = await EnviarComponentePesado(componente.IdOf.ToString(), codigoArticulo, peso);
                    if (respuesta is null)
                    {
                        LoteTranApi[] lotes = new LoteTranApi[1];
                        lotes[0] = new LoteTranApi();
                        lotes[0].Lote = componente.CantidadesPorLote.OrderBy(c => c.Cantidad).Select(l => l.Lote).FirstOrDefault();
                        lotes[0].Cantidad = decimal.Parse(peso.Replace('.', ','));
                        GuardarLineaEnTransferenciaStock(numeroOrden, codigoArticulo, lotes);
                        return await SaltarComponente2(numeroOrden, codigoArticulo, tipoProducto);
                    }
                    else
                    {
                        return QuitarTildes(respuesta);
                    }
                }
            }

            catch (Exception ex)
            {
                int intValue = 21;
                string strVal = char.ConvertFromUtf32(intValue);
                return strVal + "Se produjo un error";
            }
        }

        public async Task ObtenerTransferencia(string of)
        {
            var transferenciaBD = await context.Transferencias.Where(s => s.Estado.Equals("Pendiente")
                 && s.NumOrdenFabricacion == int.Parse(of))
                .Include(s => s.lineas)
                .ThenInclude(l => l.Lotes).FirstOrDefaultAsync();
        }
        public void GuardarLinea(string of, string articulo)
        {
            // var transferencia = context.Transferencias.Where(c => c.NumOrdenFabricacion == int.Parse(of)).FirstOrDefault();
            //var linea = context.Lineas.Where(C => C.CodArticulo.Equals(articulo)).FirstOrDefault();
            //LoteDAO nuevoLote = new LoteDAO
            //{
            //    Lote = "Lote-5",
            //    Cantidad = 120.890M,
            //    =linea.Id,
            //    Linea=linea
            //};
            //linea.Lotes.Add(nuevoLote);

            var transferencia = context.Transferencias.Include(t => t.lineas)
                .FirstOrDefault(t => t.NumOrdenFabricacion == 12345);
            if (transferencia != null)
            {
                var nuevaLinea = new LineaDAO
                {
                    CodArticulo = articulo,
                    Lotes = new List<LoteDAO>
                    {
                        // Añade los lotes necesarios aquí, si es necesario
                        new LoteDAO { Lote = "lOTE-6", Cantidad = 2.56m }, // Asume que el Id es generado automáticamente si es un campo autoincrementable, de lo contrario no lo establezcas manualmente aquí
                        new LoteDAO { Lote = "LOTE7", Cantidad = 3.56m }
                    }
                };
                transferencia.lineas.Add(nuevaLinea);
            }


            context.SaveChanges();
        }
        public TranApi ObtenerTransferenciaApi(string numOrden)
        {
            var transferencia = context.Transferencias.Include(t => t.lineas).ThenInclude(l => l.Lotes)
               .FirstOrDefault(t => t.NumOrdenFabricacion == int.Parse(numOrden) && t.Estado.Equals("Terminado"));
            //var transferencia = context.Transferencias.Where(t => t.NumOrdenFabricacion == int.Parse(numOrden)).FirstOrDefault();
            TranApi tran = _mapper.Map<TranApi>(transferencia);
            return tran;
        }
        public async Task<string> DevolverTransferenciasTerminadas()
        {
            try
            {
                string respuesta = string.Empty;
                bool existenPendientes = await context.Transferencias.Where(s => s.Estado.Equals("Terminado")).AnyAsync();
                if (existenPendientes)
                {
                    var transferencias = await context.Transferencias.Where(s => s.Estado.Equals("Terminado")).Select(o => o.NumOrdenFabricacion).ToListAsync();
                    foreach (int num in transferencias)
                    {
                        respuesta = respuesta + num.ToString() + ";";
                    }
                }
                else
                {
                    respuesta = "OK;";
                }
                return respuesta;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<string> ReenviarTransferenciasTerminadas()
        {
            string respuesta = string.Empty;
            List<int> numOrdenes = context.Transferencias.Where(s => s.Estado.Equals("Terminado"))
                .Select(n => n.NumOrdenFabricacion).ToList();
            foreach (int num in numOrdenes)
            {
                string res = await EnviarTransferenciaStock(num.ToString());
                if (res is not null)
                {
                    respuesta = respuesta + res + ";";
                }

            }
            if (respuesta.Equals(string.Empty))
            {
                return "OK;";
            }
            else
            {
                return respuesta;
            }

        }
        public async Task<string> DevolverGruposDirectorioActivo(string usuario, string contraseña)
        {
            List<string> roles = new List<string>();
            Reply reply = new Reply();
            string respuesta = string.Empty;
            var usuarioJson = new JObject(
                new JProperty("User", usuario),
                new JProperty("Pwd", contraseña),
                new JProperty("AppName", "Sistema de Balanzas")
             );
            reply = await apiController.PostLoginUsuario(usuarioJson);
            string jsonResponse = (string)reply.Data;
            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            foreach (string rol in jsonData.GruposDirectorioActivo)
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
        public async Task<List<string>> DevolverListadoNumerosOFS()
        {
            List<OrdenComponentesApi> ordenesApi = new List<OrdenComponentesApi>();
            List<string> numerosOrdenes = new List<string>();

            Reply reply = new Reply();
            reply = await apiController.GetOrdenesFabricacion<List<OrdenComponentesApi>>();
            ordenesApi = (List<OrdenComponentesApi>)reply.Data;


            foreach (var orden in ordenesApi)
            {
                numerosOrdenes.Add(orden.NumOrdenFabricacion.ToString());

            }
            return numerosOrdenes;
        }
        public async Task<string> DevolverOFs()
        {
            string linea_numeroOrdenenes = string.Empty;
            try
            {
                List<OrdenComponentesApi> ordenesApi = new List<OrdenComponentesApi>();
                List<string> numerosOrdenes = new List<string>();

                Reply reply = new Reply();
                reply = await apiController.GetOrdenesFabricacion<List<OrdenComponentesApi>>();
                ordenesApi = (List<OrdenComponentesApi>)reply.Data;


                foreach (var orden in ordenesApi)
                {
                    numerosOrdenes.Add(orden.NumOrdenFabricacion.ToString());
                    string desc = (orden.Descripcion.Length <= 15) ? orden.Descripcion : orden.Descripcion.Substring(0, 15);
                    linea_numeroOrdenenes = linea_numeroOrdenenes + " " + orden.NumOrdenFabricacion.ToString() +
                       " -" + desc + ",";
                }

                return linea_numeroOrdenenes;
            }
            catch (Exception ex)
            {
                int intValue = 21;
                string respuestaError = char.ConvertFromUtf32(intValue) + ex.Message;
                return respuestaError;
            }


        }
        public async Task<string> DevolverOFJSON(string orden)
        {
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesConComponentesJSON(orden);

            return (string)reply.Data;
        }
        public async Task<List<string>> DevolverOFsPorMP(string codigoArticulo)
        {
            string linea_numeroOrdenenes = string.Empty;
            try
            {
                List<OrdenComponentesApi> ordenesApi = new List<OrdenComponentesApi>();
                List<string> numerosOrdenes = new List<string>();

                Reply reply = new Reply();
                reply = await apiController.GetOrdenesFabricacionPorMP<List<OrdenComponentesApi>>(codigoArticulo);
                ordenesApi = (List<OrdenComponentesApi>)reply.Data;

                foreach (OrdenComponentesApi orden in ordenesApi)
                {
                    numerosOrdenes.Add(orden.NumOrdenFabricacion);
                }


                return numerosOrdenes;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public async Task<OrdenComponentesApi> DevolverOrdenComponentes(string of)
        {

            OrdenComponentesApi orden = new OrdenComponentesApi();
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesConComponentes<OrdenComponentesApi>(of);

            orden = (OrdenComponentesApi)reply.Data;

            return orden;

        }
        public async Task<string> DevolverNumeroOrdenPorPT(string codArticulo)
        {
            List<OrdenFabricacionApi> ordenesApi = new List<OrdenFabricacionApi>();
            string linea_numeroOrdenes = string.Empty;
            string respuesta = string.Empty;
            Reply reply = new Reply();
            reply = await apiController.GetOrdenesFabricacion<List<OrdenFabricacionApi>>();
            ordenesApi = (List<OrdenFabricacionApi>)reply.Data;
            foreach (OrdenFabricacionApi orden in ordenesApi)
            {
                if (orden.CodigoArticulo.Equals(codArticulo))
                {
                    string desc = (orden.Descripcion.Length <= 15) ? orden.Descripcion : QuitarTildes(orden.Descripcion.Substring(0, 15));
                    linea_numeroOrdenes = linea_numeroOrdenes + " " + orden.NumOrdenFabricacion.ToString() +
                       " -" + desc + ",";
                }
            }
            if (linea_numeroOrdenes.Equals(string.Empty))
            {
                int intValue = 21;
                respuesta = char.ConvertFromUtf32(intValue);
                respuesta += "No existe Orden de Producción liberada con el número de artículo ingresado";
            }
            else
            {
                respuesta = linea_numeroOrdenes;
            }

            return respuesta;

        }

        public string DevolverLote(ComponenteApi componente)
        {
            Balanza balanza = EscogerBalanza(componente);
            string respuesta = string.Empty;
            string orden = string.Empty;
            switch (componente.CantidadesPorLote.Count)
            {
                case 1:
                    LoteComponentesApi lote = componente.CantidadesPorLote[0];

                    respuesta = QuitarTildes(componente.Descripcion) + ";" + componente.CantidadRequerida.ToString().Replace(',', '.') + ";"
                        + balanza.NumeroBalanza + ";" + componente.CodigoArticulo + ";"
                        + componente.UnidadMedida + ";" + balanza.ToleranciaMinima + ";"
                        + balanza.ToleranciaMaxima + ";" + lote.Lote + ";" + 1 + ";"
                        + componente.LotePT + ";" + lote.FechaVence + ";" + QuitarTildes(ObtenerCodigoAnalisis(lote.AnalisisMP)) + ";" + componente.NumOrdenFabricacion + ";"
                        + RecortarCaracteres(componente.DescripcionProducto, 40) + ";";
                    break;
                case 2:
                    LoteComponentesApi loteMenorCantidad = componente.CantidadesPorLote.OrderBy(c => c.Cantidad).FirstOrDefault();
                    respuesta = componente.Descripcion + ";" + loteMenorCantidad.Cantidad.ToString().Replace(',', '.') + ";"
                        + balanza.NumeroBalanza + ";" + componente.CodigoArticulo + ";"
                        + componente.UnidadMedida + ";" + balanza.ToleranciaMinima + ";"
                        + balanza.ToleranciaMaxima + ";" + loteMenorCantidad.Lote + ";" + 2 + ";"
                        + componente.LotePT + ";" + loteMenorCantidad.FechaVence + ";" + QuitarTildes(ObtenerCodigoAnalisis(loteMenorCantidad.AnalisisMP)) + ";" + componente.NumOrdenFabricacion + ";"
                        + RecortarCaracteres(componente.DescripcionProducto, 40) + ";";


                    break;
                default:
                    int intValue = 21;
                    respuesta = char.ConvertFromUtf32(intValue);
                    respuesta += "La orden seleccionada no tiene lotes asignados";
                    break;

            }
            return respuesta;
        }
       
        

        public async Task<string> SaltarComponente(string numeroOrden, string codigoArticulo)
        {
            string respuesta = string.Empty;
            ComponenteApi componenteSiguiente = ObtenerSiguienteComponente2(numeroOrden, codigoArticulo,"");
            if (numeroOrden.Equals(ordenComponentesApiActual.NumOrdenFabricacion))
            {
                if (componenteSiguiente != null)
                {
                    return DevolverLote(componenteSiguiente);
                }
                else
                {
                    return await ValidarOrdenTerminadaPO(componentesProductoPorOrden);
                }
            }
            else
            {
                if (componenteSiguiente != null)
                {
                    return DevolverLote(componenteSiguiente);
                }
                else
                {
                    return await ValidarOrdenTerminadaPO(componentesProductoPorOrden);
                }
            }

        }

        public string DevolverSegundoLote(string numeroOrden, string codigoArticulo, string peso,string tipoProducto)
        {
            lotePesadoTemporalPO = decimal.Parse(peso.Replace('.', ','));
            ComponenteApi componente = ObtenerComponente2(numeroOrden, codigoArticulo,tipoProducto);
            if (numeroOrden.Equals(ordenComponentesApiActual.NumOrdenFabricacion))
            {

                LoteComponentesApi lote = componente.CantidadesPorLote.OrderByDescending(c => c.Cantidad).FirstOrDefault();
                Balanza balanza = EscogerBalanza(componente);
                decimal cantidadRequeridaNueva = componente.CantidadRequerida - decimal.Parse(peso.Replace('.', ','));
                return componente.Descripcion + ";" + cantidadRequeridaNueva.ToString().Replace(',', '.') + ";"
                            + balanza.NumeroBalanza + ";" + componente.CodigoArticulo + ";"
                            + componente.UnidadMedida + ";" + balanza.ToleranciaMinima + ";"
                            + balanza.ToleranciaMaxima + ";" + lote.Lote + ";" + 1 + ";"
                            + ordenComponentesApiActual.LotePT + ";" + lote.FechaVence + ";" + QuitarTildes(ObtenerCodigoAnalisis(lote.AnalisisMP)) + ";"
                            + ordenComponentesApiActual.NumOrdenFabricacion + ";";
            }
            else
            {
                LoteComponentesApi lote = componente.CantidadesPorLote.OrderByDescending(c => c.Cantidad).FirstOrDefault();
                Balanza balanza = EscogerBalanza(componente);
                decimal cantidadRequeridaNueva = componente.CantidadRequerida - decimal.Parse(peso.Replace('.', ','));
                return componente.Descripcion + ";" + cantidadRequeridaNueva.ToString().Replace(',', '.') + ";"
                            + balanza.NumeroBalanza + ";" + componente.CodigoArticulo + ";"
                            + componente.UnidadMedida + ";" + balanza.ToleranciaMinima + ";"
                            + balanza.ToleranciaMaxima + ";" + lote.Lote + ";" + 1 + ";"
                            + componente.LotePT + ";" + lote.FechaVence + ";" + QuitarTildes(ObtenerCodigoAnalisis(lote.AnalisisMP)) + ";"
                            + componente.NumOrdenFabricacion + ";";

            }


        }

        public bool SePesaronTodosLosComponentes(List<ComponenteApi> componentes)
        {
            foreach (ComponenteApi componente in componentes)
            {
                if (componente.CantidadPesada != 0)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<string> ValidarOrdenTerminadaPO(List<ComponenteApi> componentes)
        {
            string respuesta = string.Empty;
            string resApi = string.Empty;
            try
            {
                var tran = context.Transferencias.Where(n => n.NumOrdenFabricacion.Equals(componentes[0].NumOrdenFabricacion)
                && n.Estado.Equals("Pendiente")).FirstOrDefault();

                //caso contrario cargar toda la orden con los pesos

                if (tran is not null)
                {
                    tran.Estado = "Terminado";
                    context.SaveChanges();

                    resApi = await EnviarTransferenciaStock(componentes[0].NumOrdenFabricacion);
                    return resApi;
                }
                else
                {
                    int intValue = 21;
                    respuesta = char.ConvertFromUtf32(intValue);
                    respuesta += "No existe la orden en la base de datos";
                    return respuesta;
                }

            } catch (Exception ex)
            {
                int intValue = 21;
                respuesta = char.ConvertFromUtf32(intValue);
                respuesta += "Error en el guardado de la orden";
                return respuesta;
            }




        }
        public int ContarComponentesCantidadPesadaCero(List<ComponenteApi> componentes)
        {
            int cantidad = componentes.Count(c => c.CantidadPesada == 0);
            return cantidad;
        }
        
        public async Task<string> EnviarComponentePesado(string IdOf, string codArticulo, string peso)
        {
            var jsonData = new JObject(
               new JProperty("IdOf", IdOf),
               new JProperty("CodArticulo", codArticulo),
               new JProperty("CantPesada", peso)
            );
            Reply reply = new Reply();
            reply = await apiController.PostCantidadPesada<string>(jsonData);
            var jsonString = (string)reply.Data;
            JObject jsonObject = JObject.Parse(jsonString);
            string ms = (string)jsonObject["ms"];
            if (ms.Equals("True") || ms.Equals("true"))
            {
                return null;
            }
            else
            {
                int intValue = 21;
                string strVal = char.ConvertFromUtf32(intValue);
                return strVal + (string)jsonObject["Error"];
            }

        }
        public string GuardarLineaEnTransferenciaStock(string orden, string articulo, LoteTranApi[] lotes)
        {
            int numCambios = 0;
            var transferencia = context.Transferencias.Include(t => t.lineas)
                .FirstOrDefault(t => t.NumOrdenFabricacion == int.Parse(orden));
            try
            {
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
                    numCambios = context.SaveChanges();
                    return numCambios.ToString();
                }
                else
                {
                    return numCambios.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }

        public void RevisarTransferenciaDeStock(OrdenComponentesApi orden)
        {
            //validar si el numero de orden ya tiene creado una trasnferencia de stock
            bool existenTransferencia = context.Transferencias.Where(s => s.Estado.Equals("Pendiente")
            && s.NumOrdenFabricacion == int.Parse(orden.NumOrdenFabricacion)).Any();

            if (!existenTransferencia)
            {
                var tran = _mapper.Map<TransferenciaDAO>(orden);
                tran.Estado = "Pendiente";
                context.Transferencias.Add(tran);
                context.SaveChanges();
            }
        }
        public async Task<string> EnviarTransferenciaStock(string numOrden)
        {
            //enviar trasnferencia de stock
            var tran = ObtenerTransferenciaApi(numOrden);
            string respuesta = string.Empty;
            Reply reply = new Reply();
            try
            {
                reply = await apiController.PostTransferenciaStock<TranApi>(tran);
                var jsonString = (string)reply.Data;
                JObject jsonObject = JObject.Parse(jsonString);
                string id = (string)jsonObject["Id"];
                string docnum = (string)jsonObject["DocNum"];
                string error = (string)jsonObject["Error"];
                if (reply.StatusCode.Equals("OK") && error.IsNullOrEmpty())
                {
                    var orden = context.Transferencias.SingleOrDefault(o => o.NumOrdenFabricacion == int.Parse(numOrden) && o.Estado.Equals("Terminado"));
                    orden.Estado = "Enviado";
                    context.SaveChanges();
                    int intValue = 22;
                    string strVal = char.ConvertFromUtf32(intValue);
                    return strVal + id + " " + docnum;
                }
                else
                {
                    int intValue = 21;
                    string strVal = char.ConvertFromUtf32(intValue);
                    return strVal + id + " " + docnum + " " + QuitarTildes(error);
                }
            } catch (Exception ex)
            {
                int intValue = 21;
                string strVal = char.ConvertFromUtf32(intValue);
                return strVal + " No se encontró el número de orden";
            }

        }

        
        public ComponenteApi ObtenerPrimerComponente(string numOrden,string codArticulo,string tipoProducto)
        {
            if (tipoProducto.Equals("Orden"))
            {
                foreach (ComponenteApi componente in componentesProductoPorOrden)
                {
                    // Verifica si el componente actual cumple con las condiciones
                    if (componente.CodigoArticulo.Equals(codArticulo) && componente.CantidadPesada == 0)
                    {
                        return componente;
                    }
                }
            }
            else
            {
                foreach (ComponenteApi componente in componentesProductoPorCampaña)
                {
                    // Verifica si el componente actual cumple con las condiciones
                    if (componente.NumOrdenFabricacion.Equals(numOrden) && componente.CantidadPesada == 0)
                    {
                        return componente;
                    }
                }
            }
            return null;

        }
        public ComponenteApi ObtenerComponente2(string numOrden, string codArticulo, string tipoProducto)
        {
            if (tipoProducto.Equals("Orden"))
            {
                foreach (ComponenteApi componente in componentesProductoPorOrden)
                {
                    // Verifica si el componente actual cumple con las condiciones
                    if (componente.CodigoArticulo.Equals(codArticulo) && componente.CantidadPesada == 0)
                    {
                        return componente;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                _cache.TryGetValue(numOrden, out string ordenJSON);
                if (ordenJSON == null)
                {
                    OrdenComponentesApi orden = JsonConvert.DeserializeObject<OrdenComponentesApi>(ordenJSON);
                    if (orden.Componentes.Count()==0)
                    {
                        foreach(ComponenteApi componente in orden.Componentes)
                        {
                            if(componente.CodigoArticulo.Equals(codArticulo) && componente.CantidadPesada==0)
                            {
                                return componente;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public void ValidarOrden(string numOrden)
        {
            _cache.TryGetValue(numOrden, out string ordenJSON);
            if (ordenJSON == null)
            {
                
            }
        }
        public List<ComponenteApi> CompletarComponentes(OrdenComponentesApi orden)
        {
            if (orden.Componentes is not null)
            {
                foreach (ComponenteApi comp in orden.Componentes)
                {
                    comp.IdOf = orden.IdOf;
                    comp.NumOrdenFabricacion = orden.NumOrdenFabricacion;
                    comp.LotePT = orden.LotePT;
                    comp.DescripcionProducto = orden.Descripcion;
                }
                return orden.Componentes;
            }
            else
            {
                return null;
            }

        }

        public Balanza EscogerBalanza(ComponenteApi componente)
        {
            Balanza balanza = new Balanza();
            double cantidadConvertida = 0;
            switch (componente.UnidadMedida)
            {
                case "g":
                    cantidadConvertida = UnitConverter.ConvertGToKg(Convert.ToDouble(componente.CantidadRequerida));
                    break;
                case "mg":
                    cantidadConvertida = UnitConverter.ConvertMgToKg(Convert.ToDouble(componente.CantidadRequerida));
                    break;
                case "kg":
                    cantidadConvertida = Convert.ToDouble(componente.CantidadRequerida);
                    break;
            }

            if (cantidadConvertida >= ConfiguracionBalanzas.Default.Bal1_Pmin && cantidadConvertida < ConfiguracionBalanzas.Default.Bal1_Pmax)
            {
                balanza.NumeroBalanza = 1;
                balanza.PesoMinimo = ConfiguracionBalanzas.Default.Bal1_Pmin;
                balanza.PesoMaximo = ConfiguracionBalanzas.Default.Bal1_Pmax;
                balanza.ToleranciaMinima = ConfiguracionBalanzas.Default.Bal1_Tolmin;
                balanza.ToleranciaMaxima = ConfiguracionBalanzas.Default.Bal1_Tolmax;
            }
            else if (cantidadConvertida >= ConfiguracionBalanzas.Default.Bal2_Pmin && cantidadConvertida < ConfiguracionBalanzas.Default.Bal2_Pmax)
            {
                balanza.NumeroBalanza = 2;
                balanza.PesoMinimo = ConfiguracionBalanzas.Default.Bal2_Pmin;
                balanza.PesoMaximo = ConfiguracionBalanzas.Default.Bal2_Pmax;
                balanza.ToleranciaMinima = ConfiguracionBalanzas.Default.Bal2_Tolmin;
                balanza.ToleranciaMaxima = ConfiguracionBalanzas.Default.Bal2_Tolmax;
            }
            else if (cantidadConvertida >= ConfiguracionBalanzas.Default.Bal3_Pmin && cantidadConvertida < ConfiguracionBalanzas.Default.Bal3_Pmax)
            {
                balanza.NumeroBalanza = 3;
                balanza.PesoMinimo = ConfiguracionBalanzas.Default.Bal3_Pmin;
                balanza.PesoMaximo = ConfiguracionBalanzas.Default.Bal3_Pmax;
                balanza.ToleranciaMinima = ConfiguracionBalanzas.Default.Bal3_Tolmin;
                balanza.ToleranciaMaxima = ConfiguracionBalanzas.Default.Bal3_Tolmax;
            }
            else if (cantidadConvertida >= ConfiguracionBalanzas.Default.Bal4_Pmin && cantidadConvertida < ConfiguracionBalanzas.Default.Bal3_Pmax)
            {
                balanza.NumeroBalanza = 4;
                balanza.PesoMinimo = ConfiguracionBalanzas.Default.Bal4_Pmin;
                balanza.PesoMaximo = ConfiguracionBalanzas.Default.Bal4_Pmax;
                balanza.ToleranciaMinima = ConfiguracionBalanzas.Default.Bal4_Tolmin;
                balanza.ToleranciaMaxima = ConfiguracionBalanzas.Default.Bal4_Tolmax;
            }

            return balanza;
        }
        public string QuitarTildes(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return texto;
            }

            // Usamos un StringBuilder para mejorar el rendimiento al concatenar strings
            StringBuilder sb = new StringBuilder(texto.Length);

            foreach (char c in texto.Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
        public string RecortarCaracteres(string texto, int numeroCaracteres)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                if (texto.Length > numeroCaracteres)
                {
                    return texto.Substring(0, 40);
                }
                else
                {
                    return texto;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        public string ObtenerCodigoAnalisis(string texto)
        {
            // Utiliza una expresión regular para buscar el texto que comienza con "MP" y va hasta el primer espacio en blanco o hasta el último carácter
            Regex regex = new Regex(@"MP[\w-]+(?:\s|$)");
            Match match = regex.Match(texto);


            if (!string.IsNullOrWhiteSpace(texto))
            {
                if (match.Success)
                {
                    return match.Value.Trim();
                }
                else if (texto.Length >= 15)
                {
                    return texto.Substring(0, 15);
                }
                else
                {
                    return texto;
                }
            }
            else
            {
                return "_________";
            }

        }
        public string ObtenerSiguienteNumOrdenCampaña(string orden)
        {
            // Buscar el índice de la orden actual
            int indice = NumOrdenesCampaña.IndexOf(orden);

            // Verificar si la orden actual está en la lista y si no es la última
            if (indice != -1 && indice < NumOrdenesCampaña.Count - 1)
            {
                // Devolver la siguiente orden
                return NumOrdenesCampaña[indice + 1];
            }
            else
            {
                // La orden actual no está en la lista o es la última
                return null; // O puedes manejarlo de otra manera según tus necesidades
            }
        }

        //Base de datos en cache


        public async void RegistrarOrdenesCache()
        {
            var redisDB = RedisDB.Connection.GetDatabase();
            // Obtener instancia del servidor
            // IServer servidor = RedisDB.Connection.GetServer("localhost");

            // Vaciar la base de datos
            //servidor.FlushDatabase();
            List<string> numeros = await DevolverListadoNumerosOFS();
            foreach (string numero in numeros)
            {
                string orden = await DevolverOFJSON(numero);
                redisDB.StringSet(numero, orden);

            }


        }

        public async void RegistrartrarOrdenesCache2()
        {
            _cache.Clear();
            List<string> numeros = await DevolverListadoNumerosOFS();
            foreach (string numero in numeros)
            {
                string orden = await DevolverOFJSON(numero);
                _cache.Set(numero, orden);
            }
            _seTerminoCargarCache = true;
        }
        
        
        
    }
}
