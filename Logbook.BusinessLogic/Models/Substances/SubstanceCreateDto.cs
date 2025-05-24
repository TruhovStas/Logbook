namespace Logbook.BusinessLogic.Models.Substances
{
    public class SubstanceCreateDto
    {
        public string Formula { get; set; }
        public string? Method { get; set; }
        public double MolarMass { get; set; }
        public double Concentration { get; set; }
    }
}
