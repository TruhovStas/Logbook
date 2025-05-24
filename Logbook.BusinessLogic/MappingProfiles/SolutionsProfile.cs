using AutoMapper;
using Logbook.BusinessLogic.Models.Solutions;
using Logbook.Domain.Entities;

namespace Logbook.BusinessLogic.MappingProfiles
{
    public class SolutionsProfile : Profile, IMappingProfile
    {
        public SolutionsProfile()
        {
            CreateMap<Solution, SolutionResponseDto>();
            CreateMap<Solution, SolutionCreateResponseDto>();
            CreateMap<SolutionUpdateDto, Solution>();
            CreateMap<SolutionCreateDto, Solution>();
        }
    }
}
