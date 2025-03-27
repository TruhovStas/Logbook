using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Forecasts;

namespace Logbook.BusinessLogic.Services
{
    public interface IForecastService
    {
        public Task<IEnumerable<ForecastResponseDto>> GetForecastsAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<ForecastResponseDto>> GetForecastsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken);
        public Task<ForecastResponseDto> GetForecastByIdAsync(int id, CancellationToken cancellationToken);
        public Task<ForecastCreateResponseDto> CreateForecastAsync(ForecastCreateDto forecastCreateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> UpdateForecastAsync(ForecastUpdateDto forecastUpdateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> DeleteForecastAsync(int id, CancellationToken cancellationToken);
    }
}
