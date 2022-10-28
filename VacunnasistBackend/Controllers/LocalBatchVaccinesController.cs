using Microsoft.AspNetCore.Mvc;
using VacunassistBackend.Helpers;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Services;

namespace VacunassistBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocalBatchVaccinesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILocalBatchVaccinesService _localBatchVaccinesService;
        public LocalBatchVaccinesController(DataContext context, ILocalBatchVaccinesService localBatchVaccinesService, IConfiguration configuration)
        {
            this._localBatchVaccinesService = localBatchVaccinesService;
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] LocalBatchVaccinesFilterRequest filter)
        {
            return Ok(new
            {
                vaccines = _localBatchVaccinesService.GetAll(filter)
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_localBatchVaccinesService.Get(id));
        }
    }
}