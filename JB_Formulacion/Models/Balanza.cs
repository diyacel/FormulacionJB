using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models
{
    public class Balanza
    {
        public int NumeroBalanza { get; set; }
        public double PesoMinimo { get; set; }
        public double PesoMaximo { get; set; }
        public int ToleranciaMinima { get; set; }
        public int ToleranciaMaxima { get; set; }
    }
}
