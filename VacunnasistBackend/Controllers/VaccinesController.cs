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
    public class DevelopedVaccinesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IDevelopedVaccinesService _developedVaccinesService;
        private readonly IConfiguration _configuration;

        public DevelopedVaccinesController(DataContext context, IDevelopedVaccinesService vaccinesService, IConfiguration configuration)
        {
            this._developedVaccinesService = vaccinesService;
            this._configuration = configuration;
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] DevelopedVaccinesFilterRequest filter)
        {
            return Ok(new
            {
                vaccines = _developedVaccinesService.GetAll(filter)
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_developedVaccinesService.Get(id));
        }

        [HttpPost]
        public IActionResult New([FromBody] NewDevelopedVaccineRequest model)
        {
            var alreadyExist = _developedVaccinesService.AlreadyExist(model.Name);
            if (alreadyExist)
            {
                return BadRequest(new
                {
                    message = "Ya existe una vacuna con el mismo nombre"
                });

            }

            _developedVaccinesService.New(model);

            return Ok(new
            {
                message = "Vacuna creada correctamente"
            });
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int? id, [FromBody] UpdateDevelopedVaccineRequest model)
        {
            if (id == null)
            {
                return NotFound();
            }

            _developedVaccinesService.Update(id.Value, model);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/can-delete")]
        [Helpers.Authorize]
        public IActionResult CanBeDeleted(int id)
        {
            return Ok(_developedVaccinesService.CanBeDeleted(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var exist = _developedVaccinesService.Exist(id);
            if (exist == false)
            {
                return BadRequest(new
                {
                    message = "No se pudo encontrar la vacuna"
                });

            }

            if (_developedVaccinesService.CanBeDeleted(id) == false)
            {
                return BadRequest(new
                {
                    message = "No se puede eliminar la vacuna ya que se encuentra relacionada a turnos pendientes/confirmados"
                });
            }

            _developedVaccinesService.Update(id, new UpdateDevelopedVaccineRequest() { IsActive = false });

            return Ok(new
            {
                message = "Vacuna desactivada correctamente"
            });
        }
    }
}