using AutoMapper;

using ERPModels;

namespace PurchaseAPI.Infrastructure.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Purchase, PurchaseDTO>();
            CreateMap<PurchaseDTO, Purchase>();

            CreateMap<PurchaseDetail, PurchaseDetailDTO>();
            CreateMap<PurchaseDetailDTO, PurchaseDetail>();
        }
    }
}
