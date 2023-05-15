using AutoMapper;
using ERPModels;

namespace InventoryAPI.Infrastructure.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<InventoryDTO, Inventory>();
            CreateMap<Inventory, InventoryDTO>();
        }
    }
}
