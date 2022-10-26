namespace VacunassistBackend.Entities;

public class DevelopedVaccine
{
    public DevelopedVaccine()
    {
        IsActive = true;
    }

    public DevelopedVaccine(string name)
        : this()
    {
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}