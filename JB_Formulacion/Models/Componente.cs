using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class Componente
    {
        public int Id { get; set; }
       
        public string UnidadMedida { get; set; }
        public double CantidadTotal { get; set; }
        public bool RequiereRepesaje { get; set; }
        public List<CantidadPorLote> CantidadesPorLote { get; set; }
        public double CantidadPesada { get; set; }
        public string CodigoArticulo { get; set; }
        public string Descripcion { get; set; }
        public bool EsPesado { get; set; } = false;
    }
}
