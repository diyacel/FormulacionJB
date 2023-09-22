using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class OrdenComponentes
    {
        public int IdOf { get; set; }
        public int NumOrdenFabricacion { get; set; }
        public int CodArticulo { get; set; }
        public string Descripcion { get; set; }
        public string BodegaDesde { get; set; }
        public string BodegaHasta { get; set; }
        public List<Componente> Componentes { get; set; }
        public string Estado { get; set; } = "Pendiente";
    }
}
