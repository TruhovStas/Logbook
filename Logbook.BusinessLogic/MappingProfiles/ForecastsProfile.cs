using AutoMapper;
using Logbook.BusinessLogic.Models.Forecasts;
using Logbook.Domain.Entities;

namespace Logbook.BusinessLogic.MappingProfiles
{
    public class ForecastsProfile : Profile, IMappingProfile
    {
        public ForecastsProfile()
        {
            CreateMap<Forecast, ForecastResponseDto>();
            CreateMap<Forecast, ForecastCreateResponseDto>();
            CreateMap<ForecastUpdateDto, Forecast>();
            CreateMap<ForecastCreateDto, Forecast>();
        }
    }
}
