using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Infrastructure;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Utils;

namespace VacunassistBackend.Services
{
    public interface IUsersService
    {
        User Authenticate(string userName, string password);
        User Get(int id);
        User GetWithoutFetch(int id);
        User Get(string userName);
        bool Exists(string userName);
        bool Exists(int id);
        bool Register(RegisterRequest model);
        User[] GetAll(UsersFilterRequest filter);

        void AddVaccine(int id, AddVaccineRequest model);
        void DeleteVaccine(int id, int appliedVaccineId);
        void Update(int id, UpdateUserRequest model);
        bool CanBeDeleted(int id);
    }

    public class UsersService : IUsersService
    {
        private DataContext _context;

        public UsersService(DataContext context)
        {
            this._context = context;
        }
        public User Authenticate(string userName, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName && x.IsActive);
            if (user != null && PasswordHash.ValidatePassword(password, user.PasswordHash))
                return user;

            return null;
        }

        public User Get(int id)
        {
            return _context.Users.Include(u => u.Vaccines).ThenInclude(v => v.LocalBatchVaccine).ThenInclude(b => b.BatchVaccine).ThenInclude(dv => dv.DevelopedVaccine).First(x => x.Id == id);
        }

        public User GetWithoutFetch(int id)
        {
            return _context.Users.First(x => x.Id == id);
        }

        public User Get(string userName)
        {
            return _context.Users.Include(u => u.Vaccines).ThenInclude(l => l.LocalBatchVaccine).ThenInclude(b => b.BatchVaccine).ThenInclude(dv => dv.DevelopedVaccine).First(x => x.UserName == userName);
        }

        public bool Exists(string userName)
        {
            return _context.Users.Any(x => x.UserName == userName);
        }

        public bool Exists(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }

        public User[] GetAll(UsersFilterRequest filter)
        {
            var query = _context.Users.Include(u => u.Vaccines).AsQueryable();
            if (filter.IsActive.HasValue)
                query = query.Where(x => x.IsActive == filter.IsActive);
            if (string.IsNullOrEmpty(filter.Role) == false)
                query = query.Where(x => x.Role == filter.Role);
            if (string.IsNullOrEmpty(filter.UserName) == false)
                query = query.Where(x => x.UserName == filter.UserName);
            if (string.IsNullOrEmpty(filter.FullName) == false)
                query = query.Where(x => x.FullName.Contains(filter.FullName));
            if (string.IsNullOrEmpty(filter.Email) == false)
                query = query.Where(x => x.Email == filter.Email);
            return query.ToArray();
        }

        public bool Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.UserName == model.UserName))
                throw new ApplicationException("Nombre de usuario '" + model.UserName + "' en uso");

            try
            {
                var user = new User(model.UserName)
                {
                    Address = model.Address,
                    BirthDate = model.BirthDate,
                    DNI = model.DNI,
                    Email = model.Email,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Province = model.Province,
                    PasswordHash = PasswordHash.CreateHash(model.Password),
                };

                user.Role = model.Role;
                // save user
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Update(int id, UpdateUserRequest model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                throw new HttpResponseException(400, message: "Usuario no encontrado");

            if (string.IsNullOrEmpty(model.UserName) == false && model.UserName != user.UserName)
            {
                var existOther = _context.Users.Any(x => x.UserName == model.UserName && x.Id != id);
                if (existOther)
                {
                    throw new HttpResponseException(400, message: "Nombre de usuario '" + model.UserName + "' en uso");
                }
                user.UserName = model.UserName;
            }
            if (string.IsNullOrEmpty(model.Password) == false)
            {
                user.PasswordHash = PasswordHash.CreateHash(model.Password);
            }
            if (string.IsNullOrEmpty(model.FullName) == false && model.FullName != user.FullName)
            {
                user.FullName = model.FullName;
            }
            if (string.IsNullOrEmpty(model.DNI) == false && model.DNI != user.DNI)
            {
                user.DNI = model.DNI;
            }
            if (string.IsNullOrEmpty(model.Address) == false && model.Address != user.Address)
            {
                user.Address = model.Address;
            }
            if (string.IsNullOrEmpty(model.Province) == false && model.Province != user.Province)
            {
                user.Province = model.Province;
            }
            if (string.IsNullOrEmpty(model.Email) == false && model.Email != user.Email)
            {
                user.Email = model.Email;
            }
            if (string.IsNullOrEmpty(model.Gender) == false && model.Gender != user.Gender)
            {
                user.Gender = model.Gender;
            }
            if (string.IsNullOrEmpty(model.Role) == false && model.Role != user.Role)
            {
                user.Role = model.Role;
            }
            if (model.BirthDate.HasValue && model.BirthDate != user.BirthDate)
            {
                user.BirthDate = model.BirthDate.Value;
            }
            if (model.IsActive.HasValue && model.IsActive != user.IsActive)
            {
                user.IsActive = model.IsActive.Value;
            }

            _context.SaveChanges();
        }

        public void AddVaccine(int id, AddVaccineRequest model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            CheckIfExists(user);

            /*var newVaccine = new AppliedVaccine();
            newVaccine.AppliedDate = model.AppliedDate;
            newVaccine.VaccineId = model.VaccineId;
            user.Vaccines.Add(newVaccine);
            _context.SaveChanges();*/
        }

        private static void CheckIfExists(User? user)
        {
            if (user == null)
                throw new HttpResponseException(400, "Usuario no encontrado");
        }

        public void DeleteVaccine(int id, int appliedVaccineId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            CheckIfExists(user);

            if (user.Vaccines.Any(x => x.Id == appliedVaccineId) == false)
                throw new HttpResponseException(400, "Vacuna no encontrada");

            var v = user.Vaccines.Single(x => x.Id == appliedVaccineId);
            user.Vaccines.Remove(v);
            _context.SaveChanges();

        }

        public bool CanBeDeleted(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            CheckIfExists(user);
            return true;
        }
    }
}