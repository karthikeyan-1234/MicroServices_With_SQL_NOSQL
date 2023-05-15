using ERPModels;

using InventoryAPI.CQRS.Queries;
using InventoryAPI.Infrastructure.Utilities;

using MediatR;

using MongoDB.Driver;

namespace InventoryAPI.CQRS.Handlers.MongoDB
{
    public class GetInventoryByItemQueryMongoHandler : IRequestHandler<GetInventoryByItemQuery, InventoryDTO>
    {
        private IMongoCollection<InventoryDTO> collection;

        public GetInventoryByItemQueryMongoHandler(IConfiguration configuration)
        {
            MongoDBHelper<InventoryDTO> helper = new MongoDBHelper<InventoryDTO>(configuration);
            collection = helper.GetMongoDBCollection("Inventory");
        }

        public async Task<InventoryDTO> Handle(GetInventoryByItemQuery request, CancellationToken cancellationToken)
        {
            var res = await collection.FindAsync(f => f.item_id == request.item_id);
            return res.First();
        }
    }
}
