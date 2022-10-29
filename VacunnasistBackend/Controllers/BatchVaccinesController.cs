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
    public class BatchVaccinesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBatchVaccinesService _batchVaccinesService;
        public BatchVaccinesController(DataContext context, IBatchVaccinesService batchVaccinesService, IConfiguration configuration)
        {
            this._batchVaccinesService = batchVaccinesService;
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] BatchVaccinesFilterRequest filter)
        {
            return Ok(new
            {
                vaccines = _batchVaccinesService.GetAll(filter)
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_batchVaccinesService.Get(id));
        }

        [HttpPost]
        [Route("distribution")]
        public IActionResult New([FromBody] NewDistributionRequest model)
        {
            var summary = _batchVaccinesService.NewDistribution(model);

            return Ok(new
            {
                message = summary
            });
        }
    }
}