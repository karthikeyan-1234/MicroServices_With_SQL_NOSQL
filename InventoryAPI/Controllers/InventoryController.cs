using ERPModels;

using InventoryAPI.CQRS.Commands;
using InventoryAPI.Services;

using MassTransit.Mediator;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        IInventoryService service;

        public InventoryController(IInventoryService service)
        {
            this.service = service;
        }

        [HttpPost("AddNewInventory",Name = "AddNewInventory")]
        public async Task<IActionResult> AddInventory(InventoryDTO newInventory)
        {
            var res = await service.AddNewInventory(newInventory);
            return Ok(res);
        }

        [HttpGet("GetInventoryByItem",Name = "GetInventoryByItem")]
        public async Task<IActionResult> GetInventoryByItem(int item_id)
        {
            var res = await service.GetInventoryByItem(item_id);
            if(res != null)
                return Ok(res);

            return BadRequest($"Item with id {item_id} was not found in DB");
        }

        [HttpPut("UpdateInventory",Name = "UpdateInventory")]
        public async Task<IActionResult> UpdateInventory(InventoryDTO revisedInventory)
        {
            var res = await service.UpdateInventory(revisedInventory);
            if (res != null)
                return Ok(res);

            return BadRequest($"Unable to update item with id {revisedInventory.item_id} in DB");
        }
    }
}
