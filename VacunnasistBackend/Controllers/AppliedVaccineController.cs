using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacunassistBackend.Entities;
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
            if (user.Role == UserRoles.Analyst && string.IsNullOrEmpty(filter.Province))
            {
                filter.Province = user.Province;
            }     
            else if (user.Role == UserRoles.Vacunator && filter.DNI == null)
            {
                filter.AppliedById = user.Id;
            }
            return Ok(new
            {
                vaccines = _appliedVaccinesService.GetAll(filter)
            });
        }

        [HttpGet]
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

            var result = _appliedVaccinesService.New(model);
            if(result)
                return Ok(new
                {
                    message = "Aplicación realizada correctamente"
                });
            return BadRequest(new {
                message = "La vacuna para la persona con DNI " + model.DNI.ToString() + " no pudo ser aplicada."
            });
        }

    }
}
