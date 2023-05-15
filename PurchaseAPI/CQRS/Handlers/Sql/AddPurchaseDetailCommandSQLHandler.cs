using AutoMapper;

using MediatR;
using PurchaseAPI.CQRS.Commands;
using ERPModels;
using PurchaseAPI.Repositories;

namespace PurchaseAPI.CQRS.Handlers.Sql
{
    public class AddPurchaseDetailCommandSQLHandler : IRequestHandler<AddPurchaseDetailCommand, PurchaseDetailDTO>
    {
        IGenericRepo<PurchaseDetail> repo;
        IMapper mapper;

        public AddPurchaseDetailCommandSQLHandler(IGenericRepo<PurchaseDetail> repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<PurchaseDetailDTO> Handle(AddPurchaseDetailCommand request, CancellationToken cancellationToken)
        {
            var res = await repo.AddAsync(mapper.Map<PurchaseDetail>(request.purchaseDetail));
            await repo.SaveChangesAsync();
            return mapper.Map<PurchaseDetailDTO>(res);
        }
    }
}
