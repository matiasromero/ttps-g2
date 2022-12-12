using Microsoft.AspNetCore.Mvc;
using VacunassistBackend.Helpers;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Services;

namespace VacunassistBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;
        private readonly DataContext _context;
        public DepartmentsController(DataContext context, IDepartmentsService departmentsService)
        {
            this._context = context;
            this._departmentsService = departmentsService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                departments = _departmentsService.GetAll()
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_departmentsService.Get(id));
        }

        [HttpGet]
        [Route("random-by-province")]
        public IActionResult GetRandomFromProvince([FromQuery] GetDepartmentByProvinceFilter filter)
        {
            return Ok(_departmentsService.GetRandomFromProvince(filter.ProvinceName));
        }
    }
}