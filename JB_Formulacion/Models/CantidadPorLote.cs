using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    
    public class CantidadPorLote
    {
        public int Id { get; set; }
        public string Lote { get; set; }
        public double Cantidad { get; set; }
        public double CantidadPesada { get; set; }
        public double CantidadTotal { get; set; }
        public OrdenFabricacion OrdenFabricacion { get; set; }
        public MateriaPrima MateriaPrima { get; set; }
        public Transferencia Transferencia { get; set; }
    }
}
