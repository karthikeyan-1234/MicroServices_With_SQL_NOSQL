using MediatR;

using ERPModels;

namespace PurchaseAPI.CQRS.Commands
{
    public class AddPurchaseCommand : IRequest<PurchaseDTO>
    {
        public PurchaseDTO purchase { get; set; }

        public AddPurchaseCommand(PurchaseDTO purchase)
        {
            this.purchase = purchase;
        }
    }
}
