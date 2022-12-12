using Microsoft.EntityFrameworkCore;
using Quartz;

namespace VacunassistBackend.Jobs
{
    public class SyncronizeDataJob : IJob
    {
        private readonly DataContext _context;
        public SyncronizeDataJob(DataContext context)
        {
            this._context = context;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var batchVaccines = _context.BatchVaccines.Where(x => x.Synchronized == false).ToArray();
            var localBatchVaccines = _context.LocalBatchVaccines.Where(x => x.Synchronized == false).ToArray();
            var appliedVaccines = _context.AppliedVaccines.Where(x => x.Synchronized == false).ToArray();

            foreach (var batch in batchVaccines)
            {
                // Sync
                batch.Synchronized = true;
            }

            foreach (var localBatch in localBatchVaccines)
            {
                // Sync
                localBatch.Synchronized = true;
            }

            foreach (var appliedVaccine in appliedVaccines)
            {
                // Sync
                appliedVaccine.Synchronized = true;
            }

            _context.SaveChanges();

            //Write your custom code here
            return Task.CompletedTask;
        }
    }
}