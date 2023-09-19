using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class ComponenteBalanzas
    {
        public string CodigoArticulo { get; set; }
        public List<LoteBalanzas>Lotes { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public Balanza balanza { get; set; }


        


    }
}
