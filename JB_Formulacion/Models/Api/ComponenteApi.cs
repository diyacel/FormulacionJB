namespace RecepciónPesosJamesBrown.Models.Api
{
    public class ComponenteApi : OrdenComponente
    {
        public string UnidadMedida { get; set; }
        public decimal CantidadRequerida { get; set; }
        public bool RequiereRepesaje { get; set; }
        public List<LoteComponentesApi> CantidadesPorLote { get; set; }
        public decimal CantidadPesada { get; set; }
        public string CodigoArticulo { get; set; }
        public string Descripcion { get; set; }

    }
}
