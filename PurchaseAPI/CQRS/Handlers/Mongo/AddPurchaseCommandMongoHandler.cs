using AutoMapper;

using MediatR;

using PurchaseAPI.CQRS.Commands;
using ERPModels;
using PurchaseAPI.Repositories;

namespace PurchaseAPI.CQRS.Handlers.Mongo
{
    public class AddPurchaseCommandMongoHandler : IRequestHandler<AddPurchaseCommand, PurchaseDTO>
    {
        IGenericRepo<Purchase> repo;
        IMapper mapper;

        public AddPurchaseCommandMongoHandler(IGenericRepo<Purchase> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<PurchaseDTO> Handle(AddPurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchase = mapper.Map<Purchase>(request.purchase);
            var newPurchase = await repo.AddAsync(purchase);
            return mapper.Map<PurchaseDTO>(newPurchase); ;
        }
    }
}
