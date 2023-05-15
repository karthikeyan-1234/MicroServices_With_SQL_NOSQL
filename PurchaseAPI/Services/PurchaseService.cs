using AutoMapper;

using MediatR;


using PurchaseAPI.CQRS.Commands;
using PurchaseAPI.CQRS.Notifications;
using PurchaseAPI.CQRS.Queries;
using ERPModels;

using System.Text.Json;

namespace PurchaseAPI.Services
{
    public class PurchaseService : IPurchaseService
    {
        IMediator mediator;
        IMapper mapper;

        public PurchaseService(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<PurchaseDTO> AddPurchaseAsync(PurchaseDTO newPurchase)
        {
            var result = await mediator.Send(new AddPurchaseCommand(newPurchase));
            var msg = new PurchaseAddedNotification(JsonSerializer.Serialize(result));
            await mediator.Publish(msg);
            return mapper.Map<PurchaseDTO>(result);
        }

        public async Task<PurchaseDTO> GetPurchaseByID(int purchase_id)
        {
            var result = await mediator.Send(new GetPurchaseByIDQuery(purchase_id));
            return result;
        }

        public async Task<PurchaseDetailDTO> AddPurchaseDetail(PurchaseDetailDTO newPurchaseDetail)
        {
            var result = await mediator.Send(new AddPurchaseDetailCommand(newPurchaseDetail));
            var msg = new PurchaseDetailAddedNotification(JsonSerializer.Serialize(result));
            await mediator.Publish(msg);
            return result;
        }
    }
}
