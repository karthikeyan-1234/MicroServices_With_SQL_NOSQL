using AutoMapper;

using MediatR;

using PurchaseAPI.CQRS.Commands;
using ERPModels;
using PurchaseAPI.Repositories;

namespace PurchaseAPI.CQRS.Handlers.Sql
{
    public class AddPurchaseCommandSQLHandler : IRequestHandler<AddPurchaseCommand, PurchaseDTO>
    {
        IGenericRepo<Purchase> repo;
        IMapper mapper;

        public AddPurchaseCommandSQLHandler(IGenericRepo<Purchase> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<PurchaseDTO> Handle(AddPurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchase = mapper.Map<Purchase>(request.purchase);
            var newPurchase = await repo.AddAsync(purchase);
            await repo.SaveChangesAsync();
            return mapper.Map<PurchaseDTO>(newPurchase); ;
        }
    }
}
