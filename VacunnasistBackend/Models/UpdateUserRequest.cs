using System.ComponentModel.DataAnnotations;

namespace VacunassistBackend.Models
{
    public class UpdateUserRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }

        public string? Address { get; set; }
        public string? Province { get; set; }

        public string? Gender { get; set; }
        public string? Role { get; set; }

        public string? Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? DNI { get; set; }

        public bool? IsActive { get; set; }

        public bool? Pregnant { get; set; }
        public bool? HealthWorker { get; set; }
    }
}