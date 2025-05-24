namespace Logbook.Domain.Entities
{
    public class Substance : BaseEntity
    {
        public string Formula { get; set; }
        public string? Method { get; set; }
        public double MolarMass { get; set; }
        public double Concentration { get; set; }
    }
}
