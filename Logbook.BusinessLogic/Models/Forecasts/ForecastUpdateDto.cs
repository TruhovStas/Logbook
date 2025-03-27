using Logbook.Domain.Entities;

namespace Logbook.BusinessLogic.Models.Forecasts
{
    public class ForecastUpdateDto : BaseResponseDto
    {
        public DateTime DepositionDate { get; set; }
        public double Temprature1 { get; set; }
        public double Humidity1 { get; set; }
        public double Pressure1 { get; set; }
        public double Temprature2 { get; set; }
        public double Humidity2 { get; set; }
        public double Pressure2 { get; set; }
        public double TempratureBox { get; set; }
        public double HumidityBox { get; set; }
        public double PressureBox { get; set; }
        public bool ComplianceMark { get; set; }
        public IEnumerable<Equipment> Equipments { get; set; }
    }
}
