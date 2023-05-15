using AutoMapper;

using ERPModels;

using InventoryAPI.CQRS.Commands;
using InventoryAPI.Infrastructure.Utilities;

using MediatR;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System;

namespace InventoryAPI.CQRS.Handlers.MongoDB
{
    public class AddInventoryCommandMongoHandler : IRequestHandler<AddInventoryCommand, InventoryDTO>
    {
        private IMongoCollection<InventoryDTO> collection;       

        public AddInventoryCommandMongoHandler(IConfiguration configuration)
        {
            MongoDBHelper<InventoryDTO> helper = new MongoDBHelper<InventoryDTO>(configuration);
            collection = helper.GetMongoDBCollection("Inventory");
        }

        public async Task<InventoryDTO> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            request.newInventory.id = GenerateUniqueId();
            await collection.InsertOneAsync(request.newInventory);
            return request.newInventory;
        }

        public int GenerateUniqueId()
        {
            var filter = Builders<InventoryDTO>.Filter.Empty;
            var sort = Builders<InventoryDTO>.Sort.Descending(x => x.id);
            var projection = Builders<InventoryDTO>.Projection.Include(x => x.id);
            var options = new FindOptions<InventoryDTO, InventoryDTO>
            {
                Sort = sort,
                Projection = projection
            };

            var lastInventory = collection.Find(filter).FirstOrDefault();
            if (lastInventory != null)
            {
                return lastInventory.id;
            }

            return 0; // Default ID value if no documents exist

        }

    }
}
