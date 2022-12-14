using System.ComponentModel.DataAnnotations;

namespace VacunassistBackend.Models
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        public bool Pregnant { get; set; }
        public bool HealthWorker { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string DNI { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public string Province { get; set; }

    }
}