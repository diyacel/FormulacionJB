using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class DataTransferenciaStock
    {
        public string CodBodegaDesde { get; set; }
        public string CodBodegaHasta { get; set; }
        public string DocNumOF { get; set; }
        public List<Linea> Lineas { get; set; }
    }
}
