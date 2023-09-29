using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class Transferencia
    {
        public int DocNumOf { get; set; }
        public string CodBodegaDesde { get; set; }
        public string CodBodegaHasta { get; set; }
        public string Estado { get; set; }
        public List<CantidadPorLote> CantidadesPorLote { get; set; }
    }
}
