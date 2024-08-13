namespace RecepciónPesosJamesBrown.Models.DAO
{
    public class LineaDAO
    {
        public int NumOrdenFabricacion { get; set; }
        public string CodArticulo { get; set; }
        public List<LoteDAO> Lotes { get; set; }
        public TransferenciaDAO Transferencia { get; set; }
    }
}
