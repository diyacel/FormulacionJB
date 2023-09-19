using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    
    public class Lote
    {
        [JsonProperty("Lote")]
        public string NombreLote { get; set; }
        public string Cantidad { get; set; }
    }
}
