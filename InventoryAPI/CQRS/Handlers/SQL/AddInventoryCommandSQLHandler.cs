using AutoMapper;

using InventoryAPI.CQRS.Commands;
using ERPModels;
using InventoryAPI.Repositories;

using MediatR;

namespace InventoryAPI.CQRS.Handlers.SQL
{
    public class AddInventoryCommandSQLHandler : IRequestHandler<AddInventoryCommand, InventoryDTO>
    {
        IGenericRepo<Inventory> repo;
        IMapper mapper;

        public AddInventoryCommandSQLHandler(IGenericRepo<Inventory> repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<InventoryDTO> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            var res = await repo.AddAsync(mapper.Map<Inventory>(request.newInventory));
            await repo.SaveChangesAsync();
            return mapper.Map<InventoryDTO>(res);
        }
    }
}
