using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacunassistBackend.Entities;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Services;
using VacunnasistBackend.Models;
using VacunnasistBackend.Models.Filters;
using VacunnasistBackend.Services;

namespace VacunnasistBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppliedVaccineController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAppliedVaccineService _appliedVaccinesService;

        public AppliedVaccineController(DataContext context, IAppliedVaccineService appliedVaccinesService, IConfiguration configuration)
        {
            this._appliedVaccinesService = appliedVaccinesService;
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] AppliedVaccinesFilterRequest filter)
        {
            var user = (User)HttpContext.Items["User"];
            if (user.Role == UserRoles.Analyst)
            {
                filter.Province = user.Province;
            }
            else if (user.Role == UserRoles.Vacunator)
            {
                filter.AppliedById = user.Id;
            }
            return Ok(new
            {
                vaccines = _appliedVaccinesService.GetAll(filter)
            });
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_appliedVaccinesService.Get(id));
        }

        [HttpPost]
        public IActionResult New([FromBody] NewAppliedVaccineRequest model)
        {
            var user = (User)HttpContext.Items["User"];
            if (user.Role == UserRoles.Vacunator)
            {
                model.ApplyBy = user.Id;
            }

            _appliedVaccinesService.New(model);

            return Ok(new
            {
                message = "Aplicación realizada correctamente"
            });
        }

    }
}
