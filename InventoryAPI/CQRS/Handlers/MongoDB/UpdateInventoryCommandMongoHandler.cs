using ERPModels;

using InventoryAPI.CQRS.Commands;
using InventoryAPI.Infrastructure.Utilities;

using MediatR;

using MongoDB.Driver;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventoryAPI.CQRS.Handlers.MongoDB
{
    public class UpdateInventoryCommandMongoHandler : IRequestHandler<UpdateInventoryCommand, InventoryDTO>
    {
        private IMongoCollection<InventoryDTO> collection;

        public UpdateInventoryCommandMongoHandler(IConfiguration configuration)
        {
            MongoDBHelper<InventoryDTO> helper = new MongoDBHelper<InventoryDTO>(configuration);
            collection = helper.GetMongoDBCollection("Inventory");
        }

        public async Task<InventoryDTO> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<InventoryDTO>.Filter.Eq(x => x.item_id, request.inventory.item_id);
            var update = Builders<InventoryDTO>.Update
                .Set(x => x.qty, request.inventory.qty);


            var res = await collection.UpdateOneAsync(filter, update);
            return request.inventory;
        }
    }
}
