using JB_Formulacion.Helper;
using JB_Formulacion.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace JB_Formulacion.Controllers
{
    public class ApiController
    {
        string baseURL = "http://services.jbp.com.ec/api";
        string baseURLTest= "http://test.jbp.com.ec/api";
        public async Task<Reply>GetOrdenesFabricacion<T>()
        {
           var url = $"{baseURL}/of/getOfLiberadasPesaje";
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

        public async Task<Reply>GetMateriaPrima<T>()
        {
            var url = $"{baseURL}/catalogos/getCatalogoPesaje";
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
        public async Task<Reply>GetOrdenesConComponentes<T>(string NumeroOrdenFabricacion)
        {
            var url = $"{baseURL}/of/getComponentesOf/{NumeroOrdenFabricacion}";
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
            }catch(JsonSerializationException ex)
            {
                reply.Data = "La orden no tiene componentes";
                reply.StatusCode = "error";
            }
            

            return reply;
        }

        public async Task<Reply>PostTransferenciaStock<T>(DataTransferenciaStock data)
        {
            var url = $"{baseURLTest}/transferenciaStock";
            Reply reply = new Reply();
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(url,content))
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

        public async Task<Reply> PostCantidadPesada<T>(DataCantidadPesada data)
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
                        reply.Data = JsonConvert.DeserializeObject<T>(apiResponse);
                        reply.StatusCode = response.StatusCode.ToString();
                    }

                }
            }

            return reply;
        }

        public async Task<Reply> PostLoginUsuario(JObject data)
        {
            var url = $"{baseURL}/user/login";
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
