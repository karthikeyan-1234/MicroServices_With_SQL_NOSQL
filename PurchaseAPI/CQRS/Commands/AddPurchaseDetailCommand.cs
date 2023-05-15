using MediatR;

using ERPModels;

namespace PurchaseAPI.CQRS.Commands
{
    public class AddPurchaseDetailCommand:IRequest<PurchaseDetailDTO>
    {
        public PurchaseDetailDTO purchaseDetail { get; set; }

        public AddPurchaseDetailCommand(PurchaseDetailDTO purchaseDetail)
        {
            this.purchaseDetail = purchaseDetail;
        }
    }
}
