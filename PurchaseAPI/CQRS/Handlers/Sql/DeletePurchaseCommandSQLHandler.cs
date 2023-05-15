using AutoMapper;

using MediatR;

using PurchaseAPI.CQRS.Commands;
using ERPModels;
using PurchaseAPI.Repositories;

namespace PurchaseAPI.CQRS.Handlers.Sql
{
    public class DeletePurchaseCommandSQLHandler : IRequestHandler<DeletePurchaseCommand, bool>
    {
        IGenericRepo<Purchase> repo;
        IMapper mapper;

        public DeletePurchaseCommandSQLHandler(IGenericRepo<Purchase> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public Task<bool> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchase = mapper.Map<Purchase>(request.DeletePurchase);
            repo.Delete(purchase);
            return Task.FromResult(true);
        }
    }
}
