namespace Logbook.Domain.Entities
{
    public class Solution : BaseEntity
    {
        public DateOnly PreparationDate { get; set; }
        public DateOnly ValidationDate { get; set; }
        public double SolutionVolume { get; set; }
        public string StorageConditions { get; set; }
        public string StoragePeriod { get; set; }
        public string SolutionTemperature { get; set; }
        public virtual Substance Substance { get; set; }
        public virtual List<double> SubstanceMasses { get; set; } = new();
        public virtual List<double> SolutionVolumes { get; set; } = new();
        public virtual List<double> CorrectionFactors { get; set; } = new();
        public double AvgCorrectionFactor { get; set; }
        public string FIO { get; set; }
    }
}
