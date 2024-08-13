namespace RecepciónPesosJamesBrown.Models.DAO
{
    public class TransferenciaDAO
    {
        public int DocNumOF { get; set; }
        public int NumOrdenFabricacion { get; set; }
        public string CodArticulo { get; set; }
        public string Descripción { get; set; }
        public string CodBodegaDesde { get; set; }
        public string CodBodegaHasta { get; set; }
        public List<LineaDAO> lineas { get; set; }
        public string Estado { get; set; }
    }
}
