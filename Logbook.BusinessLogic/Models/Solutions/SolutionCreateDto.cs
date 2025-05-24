using Logbook.Domain.Entities;

namespace Logbook.BusinessLogic.Models.Solutions
{
    public class SolutionCreateDto
    {
        public DateOnly PreparationDate { get; set; }
        public DateOnly ValidationDate { get; set; }
        public double SolutionVolume { get; set; }
        public string StorageConditions { get; set; }
        public string StoragePeriod { get; set; }
        public string SolutionTemperature { get; set; }
        public Substance Substance { get; set; }
        public List<double> SubstanceMasses { get; set; }
        public List<double> SolutionVolumes { get; set; }
    }
}
