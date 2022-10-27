using Microsoft.AspNetCore.Mvc;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Helpers;
using VacunassistBackend.Models.Filters;

namespace VacunassistBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] VaccinesFilterRequest filter)
        {
            var vaccines = Vaccines.All;
            if (filter.Type.HasValue)
            {
                vaccines = vaccines.Where(x => x.Type == filter.Type.Value).ToArray();
            }
            return Ok(new
            {
                vaccines = vaccines
            });
        }
    }
}