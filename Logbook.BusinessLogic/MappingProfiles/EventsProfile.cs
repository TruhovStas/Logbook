using AutoMapper;
using EventsWeb.BusinessLogic.Models.Events;
using EventsWeb.Domain.Entities;

namespace EventsWeb.BusinessLogic.MappingProfiles
{
    public class EventsProfile : Profile, IMappingProfile
    {
        public EventsProfile() 
        {
            CreateMap<Event, EventResponseDto>();
            CreateMap<Event, EventCreateResponseDto>();
            CreateMap<EventUpdateDto, Event>();
            CreateMap<EventCreateDto, Event>();
        }
    }
}
