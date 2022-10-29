using Microsoft.EntityFrameworkCore;
using Quartz;

namespace VacunassistBackend.Jobs
{
    public class CheckVaccinesDueDateJob : IJob
    {
        private readonly DataContext _context;
        public CheckVaccinesDueDateJob(DataContext context)
        {
            this._context = context;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var batchVaccines = _context.BatchVaccines.Where(x => x.DueDate < DateTime.Now.Date && x.RemainingQuantity > 0).ToArray();
            var localBatchVaccines = _context.LocalBatchVaccines.Include(x => x.BatchVaccine).Where(x => x.BatchVaccine.DueDate < DateTime.Now.Date && x.RemainingQuantity > 0).ToArray();

            foreach (var batch in batchVaccines)
            {
                batch.checkOverdue();
            }

            foreach (var localBatch in localBatchVaccines)
            {
                localBatch.checkOverdue();
            }

            _context.SaveChanges();

            //Write your custom code here
            return Task.CompletedTask;
        }
    }
}