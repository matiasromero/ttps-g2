namespace VacunassistBackend.Entities
{

    public class Department
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Province Province { get; set; }
        public int ProvinceId { get; set; }
    }
}