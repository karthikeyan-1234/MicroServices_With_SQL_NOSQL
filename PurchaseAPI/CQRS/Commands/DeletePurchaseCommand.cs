using MediatR;

using ERPModels;

namespace PurchaseAPI.CQRS.Commands
{
    public class DeletePurchaseCommand: IRequest<bool>
    {
        public PurchaseDTO DeletePurchase { get; set; }

        public DeletePurchaseCommand(PurchaseDTO deletePurchase)
        {
            DeletePurchase = deletePurchase;
        }
    }
}
