using AutoMapper;
using Logbook.BusinessLogic.Models.Equipments;
using Logbook.Domain.Entities;

namespace Logbook.BusinessLogic.MappingProfiles
{
    public class EquipmentsProfile : Profile, IMappingProfile
    {
        public EquipmentsProfile() 
        {
            CreateMap<Equipment, EquipmentResponseDto>();
            CreateMap<Equipment, EquipmentCreateResponseDto>();
            CreateMap<EquipmentUpdateDto, Equipment>();
            CreateMap<EquipmentCreateDto, Equipment>();
        }
    }
}
