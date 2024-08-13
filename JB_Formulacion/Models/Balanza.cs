namespace RecepciónPesosJamesBrown.Models
{
    public class Balanza
    {
        public int NumeroBalanza { get; set; }
        public double PesoMinimo { get; set; }
        public double PesoMaximo { get; set; }
        public double ToleranciaMinima { get; set; }
        public double ToleranciaMaxima { get; set; }
    }
}
