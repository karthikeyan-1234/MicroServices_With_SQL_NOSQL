using AutoMapper;

using ERPModels;

using InventoryAPI.CQRS.Queries;
using InventoryAPI.Repositories;

using MediatR;

namespace InventoryAPI.CQRS.Handlers.SQL
{
    public class GetInventoryByItemQuerySQLHandler : IRequestHandler<GetInventoryByItemQuery, InventoryDTO>
    {
        IGenericRepo<Inventory> repo;
        IMapper mapper;

        public GetInventoryByItemQuerySQLHandler(IGenericRepo<Inventory> repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<InventoryDTO>? Handle(GetInventoryByItemQuery request, CancellationToken cancellationToken)
        {
            var res = repo.Find(i => i.item_id == request.item_id).FirstOrDefault();
            if(res != null)
            {
                return mapper.Map<InventoryDTO>(res);
            }

            return null;
        }
    }
}
