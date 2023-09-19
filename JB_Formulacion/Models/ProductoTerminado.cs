using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class ProductoTerminado
    {
        public string Codigo { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
