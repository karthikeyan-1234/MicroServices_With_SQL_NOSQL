using ERPModels;

using MediatR;

namespace InventoryAPI.CQRS.Commands
{
    public class UpdateInventoryCommand : IRequest<InventoryDTO>
    {
        public InventoryDTO ?inventory { get; set; }

        public UpdateInventoryCommand(InventoryDTO? inventory)
        {
            this.inventory = inventory;
        }
    }
}
