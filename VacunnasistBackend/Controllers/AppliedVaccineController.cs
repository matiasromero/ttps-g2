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
        [Route("patients-count")]
        public IActionResult GetPatientsCount([FromQuery] AppliedVaccinesFilterRequest filter)
        {
            var vaccines = _appliedVaccinesService.GetAll(filter);
            var patients = vaccines.Select(x => x.PatientId).Distinct().Count();
            return Ok(new
            {
                patientsCount = patients
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

            var departmentExist = _context.Departments.Any(x => x.Name == model.Department);
            if (!departmentExist)
                return BadRequest(new
                {
                    message = "Departamento " + model.Department + " no encontrado."
                });

            var result = _appliedVaccinesService.New(model);
            if (result.Item1)
                return Ok(new
                {
                    message = "Aplicación realizada correctamente. " + result.Item2
                });
            return BadRequest(new
            {
                message = result.Item2
            });
        }

    }
}
