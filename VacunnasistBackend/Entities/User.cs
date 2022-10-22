namespace VacunassistBackend.Entities;

public class User
{
    public User()
    {
        IsActive = true;
        Vaccines = new List<AppliedVaccine>();
    }

    public User(string username)
        : this()
    {
        UserName = username;
    }
    public int Id { get; set; }

    public string? PasswordHash { get; set; }

    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Age
    {
        get
        {
            var now = DateTime.Today;
            int age = now.Year - BirthDate.Year;
            if (now < BirthDate.AddYears(age))
                age--;
            return age;
        }
    }

    public bool Pregnant { get; set; }
    public bool HealthWorker { get; set; }

    public string DNI { get; set; }

    public string Gender { get; set; }

    public string Role { get; set; } = string.Empty;

    public bool IsActive { get; set; }
    public virtual List<AppliedVaccine> Vaccines { get; set; }

    public virtual int GetAge()
    {
        int Age = (int)(DateTime.Today - BirthDate).TotalDays;
        Age = Age / 365;
        return Age;
    }
}

public static class UserRoles
{
    public static string Administrator = "administrator";
    public static string Operator = "operator";
    public static string Vacunator = "vacunator";
    public static string Patient = "patient";
}

public static class Province
{
    public static string BuenosAires = "Buenos Aires";
    public static string Catamarca = "Catamarca";
    public static string Chaco = "Chaco";
    public static string Chubut = "Chubut";
    public static string Cordoba = "Córdoba";
    public static string Corrientes = "Corrientes";
    public static string EntreRios = "Entre Ríos";
    public static string Formosa = "Formosa";
    public static string Jujuy = "Jujuy";
    public static string LaPampa = "La Pampa";
    public static string LaRioja = "La Rioja";
    public static string Mendoza = "Mendoza";
    public static string Misiones = "Misiones";
    public static string Neuquen = "Neuquén";
    public static string RioNegro = "Río Negro";
    public static string Salta = "Salta";
    public static string SanJuan = "San Juan";
    public static string SanLuis = "San Luis";
    public static string SantaCruz = "Santa Cruz";
    public static string SantaFe = "Santa Fe";
    public static string SantiagoDelEstero = "Santiago del Estero";
    public static string TierraDelFuego = "Tierra del Fuego";
    public static string Tucuman = "Tucumán";
}

public static class Gender
{
    public static string Male = "male";
    public static string Female = "female";
    public static string Other = "other";
}