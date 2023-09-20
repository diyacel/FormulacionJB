using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class ComponenteBalanzas: Componente
    {
        public Balanza Balanza { get; set; }
        public double CantidadLote { get; set; }
        public double CantidadPesadaLote {  get; set; }
        public string NombreLote {  get; set; }
        public string Estado { get; set; }
    }
}
