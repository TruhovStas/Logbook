using Logbook.Domain.Entities;

namespace Logbook.BusinessLogic.Models.Solutions
{
    public class SolutionResponseDto : BaseResponseDto
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
        public virtual List<double> SubstanceMasses { get; set; } = new();
        public virtual List<double> SubstanceVolumes { get; set; } = new();
        public virtual List<double> CorrectionFactors { get; set; } = new();
        public double AvgCorrectionFactor { get; set; }
        public string Login { get; set; }
    }

    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
