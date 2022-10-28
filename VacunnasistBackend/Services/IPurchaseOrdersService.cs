using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Infrastructure;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Utils;

namespace VacunassistBackend.Services
{
    public interface IPurchaseOrdersService
    {
        PurchaseOrder[] GetAll(PurchaseOrdersFilterRequest filter);
        PurchaseOrder Get(int id);
        void Update(int id, UpdatePurchaseOrderRequest model);
        bool New(NewPurchaseOrderRequest model);
        bool Exist(int id);
    }

    public class PurchaseOrdersService : IPurchaseOrdersService
    {
        private DataContext _context;

        public PurchaseOrdersService(DataContext context)
        {
            this._context = context;
        }

        public PurchaseOrder[] GetAll(PurchaseOrdersFilterRequest filter)
        {
            var query = _context.PurchaseOrders.Include(x => x.DevelopedVaccine).AsQueryable();
            if (filter.Status.HasValue)
                query = query.Where(x => x.Status == filter.Status);
            if (string.IsNullOrEmpty(filter.BatchNumber) == false)
                query = query.Where(x => x.BatchNumber.ToUpper().Contains(filter.BatchNumber.ToUpper()));
            if (filter.ETA.HasValue)
                query = query.Where(x => x.ETA.HasValue && x.ETA.Value.Date == filter.ETA.Value.Date);
            if (filter.PurchaseDate.HasValue)
                query = query.Where(x => x.PurchaseDate.Date == filter.PurchaseDate.Value.Date);
            return query.ToArray();
        }

        public bool New(NewPurchaseOrderRequest model)
        {
            try
            {
                var batchNumber = RandomGenerator.RandomString(12);
                var po = new PurchaseOrder(batchNumber, model.Quantity, model.DevelopedVaccineId);
                _context.PurchaseOrders.Add(po);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PurchaseOrder Get(int id)
        {
            return _context.PurchaseOrders.First(x => x.Id == id);
        }

        public void Update(int id, UpdatePurchaseOrderRequest model)
        {
            var po = _context.PurchaseOrders.Include(x => x.DevelopedVaccine).FirstOrDefault(x => x.Id == id);
            if (po == null)
                throw new HttpResponseException(400, message: "Orden de compra no encontrada");

            if (model.Status.HasValue && model.Status != po.Status)
            {
                po.ChangeStatus(model.Status.Value);
                if (po.Status == PurchaseStatus.Delivered)
                {
                    var random = new Random();
                    var newBatch = new BatchVaccine(po.BatchNumber, po.Quantity)
                    {
                        DevelopedVaccineId = po.DevelopedVaccineId,
                        DueDate = DateTime.Now.Date.AddDays(random.Next(5, 60)),
                        PurchaseOrderId = po.Id
                    };
                    _context.BatchVaccines.Add(newBatch);
                }
            }

            _context.SaveChanges();
        }

        public bool Exist(int id)
        {
            return _context.PurchaseOrders.Any(x => x.Id == id);
        }
    }
}