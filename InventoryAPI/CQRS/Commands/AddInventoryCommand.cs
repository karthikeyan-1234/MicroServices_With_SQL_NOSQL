using ERPModels;

using MediatR;

namespace InventoryAPI.CQRS.Commands
{
    public class AddInventoryCommand : IRequest<InventoryDTO>
    {
        public InventoryDTO newInventory { get; set; }

        public AddInventoryCommand(InventoryDTO newInventory)
        {
            this.newInventory = newInventory;
        }
    }
}
