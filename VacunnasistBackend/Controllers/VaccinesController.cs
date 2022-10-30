using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly DataContext _context;
        public VaccinesController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] VaccinesFilterRequest filter)
        {
            var vaccines = Vaccines.All;
            if (filter.Type.HasValue)
            {
                vaccines = vaccines.Where(x => x.Type == filter.Type.Value).ToArray();
            }
            if (filter.WithStock.HasValue && filter.WithStock.Value)
            {
                var developedVaccines = this._context.BatchVaccines.Where(x => x.RemainingQuantity > 0 && x.Status == Entities.BatchStatus.Valid)
                .Select(x => x.DevelopedVaccine).ToArray();
                vaccines = vaccines.Where(x => developedVaccines.Select(x => x.Vaccine.Id).Contains(x.Id)).ToArray();
            }
            return Ok(new
            {
                vaccines = vaccines
            });
        }
    }
}