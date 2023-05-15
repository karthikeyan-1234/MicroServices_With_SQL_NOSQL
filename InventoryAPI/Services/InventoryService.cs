using InventoryAPI.CQRS.Commands;
using ERPModels;

using MediatR;
using InventoryAPI.CQRS.Queries;

namespace InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        IMediator mediator;

        public InventoryService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<InventoryDTO> AddNewInventory(InventoryDTO newInventory)
        {
            var cmd = new AddInventoryCommand(newInventory);
            var res = await mediator.Send(cmd);
            return res;
        }

        public async Task<InventoryDTO> GetInventoryByItem(int item_id)
        {
            var qry = new GetInventoryByItemQuery(item_id);
            var res = await mediator.Send(qry);
            return res;
        }

        public async Task<InventoryDTO> UpdateInventory(InventoryDTO revisedInventory)
        {
            var cmd = new UpdateInventoryCommand(revisedInventory);
            var res = await mediator.Send(cmd);
            return res;
        }
    }
}
