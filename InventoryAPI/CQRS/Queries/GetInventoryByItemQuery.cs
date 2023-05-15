using ERPModels;

using MediatR;

namespace InventoryAPI.CQRS.Queries
{
    public class GetInventoryByItemQuery : IRequest<InventoryDTO>
    {
        public int item_id { get; set; }

        public GetInventoryByItemQuery(int item_id)
        {
            this.item_id = item_id;
        }
    }
}
