using AutoMapper;
using Logbook.BusinessLogic.Models.Substances;
using Logbook.Domain.Entities;

namespace Logbook.BusinessLogic.MappingProfiles
{
    public class SubstancesProfile : Profile, IMappingProfile
    {
        public SubstancesProfile()
        {
            CreateMap<Substance, SubstanceResponseDto>();
            CreateMap<Substance, SubstanceCreateResponseDto>();
            CreateMap<SubstanceUpdateDto, Substance>();
            CreateMap<SubstanceCreateDto, Substance>();
        }
    }
}
