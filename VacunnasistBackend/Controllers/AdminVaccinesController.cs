using Microsoft.AspNetCore.Mvc;
using Quartz;
using VacunassistBackend.Helpers;

namespace VacunassistBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminVaccinesController : ControllerBase
    {
        private readonly ISchedulerFactory _factory;
        public AdminVaccinesController(ISchedulerFactory factory)
        {
            this._factory = factory;
        }

        [HttpPost]
        [Route("fire-cron")]
        public async Task<OkObjectResult> FireCron()
        {
            IScheduler scheduler = await _factory.GetScheduler();

            await scheduler.TriggerJob(new JobKey("SyncronizeDataJob"));

            return Ok(new OkObjectResult("Ok"));
        }
    }
}