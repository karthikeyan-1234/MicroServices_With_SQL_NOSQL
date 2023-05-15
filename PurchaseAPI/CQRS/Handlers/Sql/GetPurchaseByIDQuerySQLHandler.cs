using AutoMapper;

using MediatR;

using PurchaseAPI.CQRS.Queries;
using ERPModels;
using PurchaseAPI.Repositories;

namespace PurchaseAPI.CQRS.Handlers.Sql
{
    public class GetPurchaseByIDQuerySQLHandler : IRequestHandler<GetPurchaseByIDQuery, PurchaseDTO>
    {
        IGenericRepo<Purchase> repo;
        IMapper mapper;

        public GetPurchaseByIDQuerySQLHandler(IGenericRepo<Purchase> repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public Task<PurchaseDTO> Handle(GetPurchaseByIDQuery request, CancellationToken cancellationToken)
        {
           var res = repo.Find(p => p.id == request.purchase_id).FirstOrDefault();
            return Task.FromResult(mapper.Map<PurchaseDTO>(res));
        }
    }
}
