namespace VacunassistBackend.Services
{
    public interface INotificationsService
    {
        void Trigger();
        void Trigger(int id);
    }

    public class NotificationsService : INotificationsService
    {
        private DataContext _context;
        private readonly IConfiguration _configuration;


        public NotificationsService(DataContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }


        public void Trigger()
        {
            DoTrigger(new int[] { });
        }

        public void Trigger(int id)
        {
            DoTrigger(new int[] { id });
        }

        private void DoTrigger(int[] ids)
        {

        }
    }
}