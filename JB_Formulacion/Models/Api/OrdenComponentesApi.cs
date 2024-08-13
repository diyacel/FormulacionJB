namespace RecepciónPesosJamesBrown.Models.Api
{
    public class OrdenComponentesApi
    {
        public int IdOf { get; set; }
        public string NumOrdenFabricacion { get; set; }
        public string CodArticulo { get; set; }
        public string Descripcion { get; set; }
        public string BodegaDesde { get; set; }
        public string BodegaHasta { get; set; }
        public string LotePT { get; set; }
        public List<ComponenteApi> Componentes { get; set; }


    }
}
