namespace RecepciónPesosJamesBrown.Models.DAO
{
    public class LoteDAO
    {
        public int Id { get; set; }
        public int NumOrdenFabricacion { get; set; }
        public string CodArticulo { get; set; }
        public string Lote { get; set; }
        public decimal Cantidad { get; set; }
        public LineaDAO Linea { get; set; }

    }
}
