using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacunassistBackend.Entities;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Services;
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
            if (user.Role == UserRoles.Vacunator)
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

        /*[HttpPost]
        public IActionResult NewVaccineApplication([FromBody] NewAppliedVaccineRequest model)
        {
            // var alreadyExist = _developedVaccinesService.AlreadyExist(model.Name);
            // if (alreadyExist)
            // {
            //     return BadRequest(new
            //     {
            //         message = "Ya existe una vacuna desarrollada con el mismo nombre"
            //     });

            // }

            // _developedVaccinesService.New(model);

            return Ok(new
            {
                message = "Vacuna desarrollada creada correctamente"
            });
        }*/

    }
}
