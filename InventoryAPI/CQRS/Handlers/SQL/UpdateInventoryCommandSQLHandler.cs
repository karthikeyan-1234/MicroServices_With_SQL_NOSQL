using AutoMapper;

using ERPModels;
using InventoryAPI.CQRS.Commands;
using InventoryAPI.Repositories;

using MediatR;

namespace InventoryAPI.CQRS.Handlers.SQL
{
    public class UpdateInventoryCommandSQLHandler : IRequestHandler<UpdateInventoryCommand, InventoryDTO>
    {
        IGenericRepo<Inventory> repo;
        IMapper mapper;

        public UpdateInventoryCommandSQLHandler(IGenericRepo<Inventory> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<InventoryDTO?> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var res = repo.Find(i => i.item_id == request.inventory.item_id).FirstOrDefault();

            if (res != null)
            {
                await repo.UpdateAsync(mapper.Map<Inventory>(request.inventory));
                await repo.SaveChangesAsync();
                return request.inventory;
            }

            return null;
        }
    }
}
