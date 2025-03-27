using AutoMapper;
using Logbook.BusinessLogic.Exceptions;
using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Forecasts;
using Logbook.DataAccess;
using Logbook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Logbook.BusinessLogic.Services.Impl
{
    public class ForecastService : IForecastService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ForecastService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<ForecastResponseDto>> GetForecastsAsync(CancellationToken cancellationToken)
        {
            var forecasts = await _unitOfWork.Forecasts.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ForecastResponseDto>>(forecasts);
        }

        public async Task<IEnumerable<ForecastResponseDto>> GetForecastsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            var forecasts = await _unitOfWork.Forecasts.GetByPageAsync(page, pageSize, cancellationToken);
            return _mapper.Map<IEnumerable<ForecastResponseDto>>(forecasts);
        }

        public async Task<ForecastResponseDto> GetForecastByIdAsync(int id, CancellationToken cancellationToken)
        {
            var forecast = await _unitOfWork.Forecasts.GetByIdAsync(id, cancellationToken);
            if (forecast == null)
                throw new NotFoundException("Оборудование не найдено.");
            return _mapper.Map<ForecastResponseDto>(forecast);
        }

        public async Task<ForecastCreateResponseDto> CreateForecastAsync(ForecastCreateDto forecastCreateDto,
            CancellationToken cancellationToken)
        {
            var forecast = _mapper.Map<Forecast>(forecastCreateDto);
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                throw new UnauthorizedAccessException("Не удалось получить имя пользователя из токена.");
            }

            forecast.Login = userName;
            var createdForecast = await _unitOfWork.Forecasts.AddAsync(forecast, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ForecastCreateResponseDto>(createdForecast);
        }

        public async Task<BaseResponseDto> UpdateForecastAsync(ForecastUpdateDto forecastUpdateDto,
            CancellationToken cancellationToken)
        {
            var forecast = await _unitOfWork.Forecasts.GetByIdAsync(forecastUpdateDto.Id, cancellationToken);
            if (forecast == null)
                throw new NotFoundException("Оборудование не найдено.");
            _mapper.Map(forecastUpdateDto, forecast);
            var updatedForecast = _unitOfWork.Forecasts.Update(forecast);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = updatedForecast.Id };
        }

        public async Task<BaseResponseDto> DeleteForecastAsync(int id, CancellationToken cancellationToken)
        {
            var forecast = await _unitOfWork.Forecasts.GetByIdAsync(id, cancellationToken);
            if (forecast == null)
                throw new NotFoundException("Forecast not found");
            _unitOfWork.Forecasts.Delete(forecast);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = id };
        }
    }
}
