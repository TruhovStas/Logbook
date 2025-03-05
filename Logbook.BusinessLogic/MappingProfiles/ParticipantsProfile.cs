using AutoMapper;
using EventsWeb.BusinessLogic.Models.Participants;
using EventsWeb.Domain.Entities;

namespace EventsWeb.BusinessLogic.MappingProfiles
{
    public class ParticipantsProfile : Profile, IMappingProfile
    {
        public ParticipantsProfile() 
        {
            CreateMap<ParticipantCreateDto, Participant>();
            CreateMap<Participant, ParticipantCreateResponseDto>();
            CreateMap<Participant, ParticipantResponseDto>();
        }
    }
}
