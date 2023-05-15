using MediatR;

using ERPModels;

namespace PurchaseAPI.CQRS.Queries
{
    public class GetPurchaseByIDQuery : IRequest<PurchaseDTO>
    {
        public int purchase_id { get; set; }

        public GetPurchaseByIDQuery(int purchase_id)
        {
            this.purchase_id = purchase_id;
        }
    }
}
