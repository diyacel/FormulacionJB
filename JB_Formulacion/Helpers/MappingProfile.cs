using AutoMapper;
using RecepciónPesosJamesBrown.Models.Api;
using RecepciónPesosJamesBrown.Models.DAO;
using System.Reflection;

namespace RecepciónPesosJamesBrown.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<TransferenciaDAO, TranApi>()
                .ForMember(c => c.DocNumOF, o => o.MapFrom(s => s.DocNumOF))
                .ForMember(c => c.CodBodegaHasta, o => o.MapFrom(s => s.CodBodegaHasta))
                .ForMember(c => c.CodBodegaDesde, o => o.MapFrom(s => s.CodBodegaDesde))
                .ForMember(c => c.Lineas, o => o.MapFrom(s => s.lineas));

            CreateMap<LineaDAO, LineaTranApi>()
                .ForMember(c => c.CodArticulo, o => o.MapFrom(s => s.CodArticulo))
                .ForMember(c => c.Lotes, o => o.MapFrom(s => s.Lotes));

            CreateMap<LoteDAO, LoteTranApi>()
                .ForMember(c => c.Lote, o => o.MapFrom(s => s.Lote))
                .ForMember(c => c.Cantidad, o => o.MapFrom(s => s.Cantidad));

            CreateMap<OrdenComponentesApi, TransferenciaDAO>()
                .ForMember(c => c.DocNumOF, o => o.MapFrom(s => s.IdOf))
                .ForMember(c => c.NumOrdenFabricacion, o => o.MapFrom(s => s.NumOrdenFabricacion))
                .ForMember(c => c.CodArticulo, o => o.MapFrom(s => s.CodArticulo))
                .ForMember(c => c.Descripción, o => o.MapFrom(s => s.Descripcion))
                .ForMember(c => c.CodBodegaHasta, o => o.MapFrom(s => s.BodegaHasta))
                .ForMember(c => c.CodBodegaDesde, o => o.MapFrom(s => s.BodegaDesde));



        }
    }
}
