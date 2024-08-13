namespace RecepciónPesosJamesBrown.Models.Api
{
    public class TranApi
    {
        public string CodBodegaDesde { get; set; }
        public string CodBodegaHasta { get; set; }
        public int DocNumOF { get; set; }
        public List<LineaTranApi> Lineas { get; set; }
    }
}
