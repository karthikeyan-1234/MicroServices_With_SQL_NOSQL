using AutoMapper;

using MediatR;
using PurchaseAPI.CQRS.Commands;
using PurchaseAPI.CQRS.Queries;

using ERPModels;
using PurchaseAPI.Repositories;

namespace PurchaseAPI.CQRS.Handlers.Sql
{
    public class GetAllPurchasesQuerySQLHandler : IRequestHandler<GetAllPurchasesQuery, IEnumerable<PurchaseDTO>>
    {
        IGenericRepo<Purchase> repo;
        IMapper mapper;

        public GetAllPurchasesQuerySQLHandler(IGenericRepo<Purchase> repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseDTO>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
        {
            var res = mapper.Map<IEnumerable<PurchaseDTO>>(await repo.GetAllAsync());
            return res;
        }
    }
}
