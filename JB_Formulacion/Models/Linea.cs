using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class Linea
    {
        public string CodArticulo { get; set; }
        public List<CantidadPorLote> Lotes { get; set; }
    }
}
