using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PurchaseAPI.CQRS.Commands;
using PurchaseAPI.CQRS.Queries;
using ERPModels;
using PurchaseAPI.Services;

namespace PurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        IPurchaseService service;

        public PurchaseController(IPurchaseService service)
        {
            this.service = service;
        }

        [HttpPost("AddPurchase")]
        public async Task<IActionResult> AddPurchase(PurchaseDTO newPurchase)
        {
            var res = await service.AddPurchaseAsync(newPurchase);
            return Ok(res);
        }

        [HttpPost("AddPurchaseDetail")]
        public async Task<IActionResult> AddPurchaseDetail(PurchaseDetailDTO newPurchaseDetail)
        {
            var res = await service.AddPurchaseDetail(newPurchaseDetail);
            return Ok(res);
        }

        [HttpGet("GetPurchaseByID")]
        public async Task<IActionResult> GetPurchaseById(int id)
        {
            var res = await service.GetPurchaseByID(id);
            return Ok(res);
        }
    }
}
