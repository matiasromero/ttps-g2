using Microsoft.AspNetCore.Mvc;
using VacunassistBackend.Helpers;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Services;

namespace VacunassistBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPurchaseOrdersService _purchaseOrderService;

        public PurchaseOrdersController(DataContext context, IPurchaseOrdersService purchaseOrdersService)
        {
            this._purchaseOrderService = purchaseOrdersService;
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PurchaseOrdersFilterRequest filter)
        {
            return Ok(new
            {
                purchaseOrders = _purchaseOrderService.GetAll(filter)
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_purchaseOrderService.Get(id));
        }

        [HttpPost]
        public IActionResult New([FromBody] NewPurchaseOrderRequest model)
        {
            _purchaseOrderService.New(model);

            return Ok(new
            {
                message = "Orden de compra creada correctamente"
            });
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int? id, [FromBody] UpdatePurchaseOrderRequest model)
        {
            if (id == null)
            {
                return NotFound();
            }

            _purchaseOrderService.Update(id.Value, model);
            return Ok();
        }
    }
}