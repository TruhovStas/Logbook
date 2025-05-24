namespace Logbook.BusinessLogic.Models.Substances
{
    public class SubstanceResponseDto : BaseResponseDto
    {
        public string Formula { get; set; }
        public string? Method { get; set; }
        public double MolarMass { get; set; }
        public double Concentration { get; set; }
    }

    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
