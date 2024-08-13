namespace RecepciónPesosJamesBrown.Models.Api
{
    public class LineaTranApi
    {
        public string CodArticulo { get; set; }
        public List<LoteTranApi> Lotes { get; set; }
    }
}
