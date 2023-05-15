using MediatR;

using ERPModels;

namespace PurchaseAPI.CQRS.Queries
{
    public class GetAllPurchasesQuery : IRequest<IEnumerable<PurchaseDTO>>
    {
    }
}
