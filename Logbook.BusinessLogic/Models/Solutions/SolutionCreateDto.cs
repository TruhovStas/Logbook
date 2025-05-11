namespace Logbook.BusinessLogic.Models.Solutions
{
    public class SolutionCreateDto
    {
        public DateOnly PreparationDate { get; set; }
        public DateOnly ValidationDate { get; set; }
        public double SolutionVolume { get; set; }
        public string StorageConditions { get; set; }
        public string StoragePeriod { get; set; }
        public double SolutionTemperature { get; set; }
        public string Substance { get; set; }
        public double SubstanceMolarMass { get; set; }
        public double SubstanceConcentration { get; set; }
        public List<double> SubstanceMasses { get; set; }
        public List<double> SubstanceVolumes { get; set; }
    }
}
