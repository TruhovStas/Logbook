namespace Logbook.Domain.Entities
{
    public class Equipment : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}
