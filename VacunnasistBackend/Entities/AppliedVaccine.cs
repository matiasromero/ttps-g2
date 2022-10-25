namespace VacunassistBackend.Entities;

public class AppliedVaccine
{
    public int Id { get; set; }
    public DateTime? AppliedDate { get; set; }
    public string? AppliedBy { get; set; }
    public string? Comment { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int VaccineId { get; set; }
    public DevelopedVaccine Vaccine { get; set; }
}