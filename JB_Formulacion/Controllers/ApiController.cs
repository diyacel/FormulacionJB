using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecepciónPesosJamesBrown.Helpers;
using RecepciónPesosJamesBrown.Models.Api;
using System.Text;

namespace RecepciónPesosJamesBrown.Controllers
{
    public class ApiController
    {
        string baseURL = "http://services.jbp.com.ec/api";
        string baseURLTest = "http://test.jbp.com.ec/api";
        public async Task<Reply> GetOrdenesFabricacion<T>()
        {
            var url = $"{baseURLTest}/of/getOfLiberadasPesaje";
            Reply reply = new Reply();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reply.Data = JsonConvert.DeserializeObject<T>(apiResponse);
                        reply.StatusCode = response.StatusCode.ToString();
                    }

                }
            }

            return reply;
        }

        public async Task<Reply> GetOrdenesFabricacionPorMP<T>(string codigoArticulo)
        {
            var url = $"{baseURLTest}/of/getOfLiberadasPesaje/{codigoArticulo}";
            Reply reply = new Reply();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reply.Data = JsonConvert.DeserializeObject<T>(apiResponse);
                        reply.StatusCode = response.StatusCode.ToString();
                    }

                }
            }

            return reply;
        }

        public async Task<Reply> GetMateriaPrima<T>()
        {
            var url = $"{baseURLTest}/catalogos/getCatalogoPesaje";
            Reply reply = new Reply();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reply.Data = JsonConvert.DeserializeObject<T>(apiResponse);
                        reply.StatusCode = response.StatusCode.ToString();
                    }

                }
            }

            return reply;
        }

        public async Task<Reply> GetOrdenesConComponentes<T>(string NumeroOrdenFabricacion)
        {
            var url = $"{baseURLTest}/of/getComponentesOf/{NumeroOrdenFabricacion}";
            Reply reply = new Reply();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            reply.Data = JsonConvert.DeserializeObject<T>(apiResponse);
                            reply.StatusCode = response.StatusCode.ToString();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                reply.Data = "Orden no disponible para pesar";
                reply.StatusCode = "Error";
            }


            return reply;
        }
        public async Task<Reply> GetOrdenesConComponentesJSON(string NumeroOrdenFabricacion)
        {
            var url = $"{baseURLTest}/of/getComponentesOf/{NumeroOrdenFabricacion}";
            Reply reply = new Reply();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            reply.Data = apiResponse;
                            reply.StatusCode = response.StatusCode.ToString();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                reply.Data = "Orden no disponible para pesar";
                reply.StatusCode = "Error";
            }


            return reply;
        }


        public async Task<Reply> PostTransferenciaStock<T>(TranApi data)
        {
            var url = $"{baseURLTest}/transferenciaStock";
            Reply reply = new Reply();
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reply.Data = apiResponse;//JsonConvert.DeserializeObject<T>(apiResponse);
                        reply.StatusCode = response.StatusCode.ToString();
                    }

                }
            }

            return reply;
        }

        public async Task<Reply> PostCantidadPesada<T>(JObject data)
        {
            var url = $"{baseURLTest}/setCantPesadaComponenteOF";
            Reply reply = new Reply();
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reply.Data = apiResponse;
                        reply.StatusCode = response.StatusCode.ToString();
                    }

                }
            }

            return reply;
        }

        public async Task<Reply> PostLoginUsuario(JObject data)
        {
            var url = $"{baseURLTest}/user/login";
            Reply reply = new Reply();
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reply.Data = apiResponse;
                        reply.StatusCode = response.StatusCode.ToString();
                    }

                }
            }

            return reply;
        }

    }
}

