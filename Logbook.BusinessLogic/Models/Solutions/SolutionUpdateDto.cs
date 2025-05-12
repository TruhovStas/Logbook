namespace Logbook.BusinessLogic.Models.Solutions
{
    public class SolutionUpdateDto : BaseResponseDto
    {
        public DateOnly PreparationDate { get; set; }
        public DateOnly ValidationDate { get; set; }
        public double SolutionVolume { get; set; }
        public string StorageConditions { get; set; }
        public string StoragePeriod { get; set; }
        public string SolutionTemperature { get; set; }
        public string Substance { get; set; }
        public double SubstanceMolarMass { get; set; }
        public double SubstanceConcentration { get; set; }
        public IEnumerable<double> SubstanceMasses { get; set; }
        public IEnumerable<double> SubstanceVolumes { get; set; }
    }
}
