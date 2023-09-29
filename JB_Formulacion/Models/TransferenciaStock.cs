using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class TransferenciaStock
    {
        public int DocNumOf { get; set; }
        public string CodBodegaDesde { get; set; }
        public string CodBodegaHasta { get; set; }
        List<Linea> Lineas { get; set; }
    }
    public class Linea
    {
        public int CodArticulo { get; set; }
        public List<Lote> Lotes { get; set; }
    }
    public class Lote
    {
        public string CodigoLote { get; set; }
        public double Cantidad { get; set; }
    }
}
