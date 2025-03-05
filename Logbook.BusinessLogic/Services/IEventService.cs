using EventsWeb.BusinessLogic.Models;
using EventsWeb.BusinessLogic.Models.Events;

namespace EventsWeb.BusinessLogic.Services
{
    public interface IEventService
    {
        public Task<IEnumerable<EventResponseDto>> GetEventsAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<EventResponseDto>> GetEventsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken);
        public Task<EventResponseDto> GetEventByIdAsync(int id, CancellationToken cancellationToken);
        public Task<EventCreateResponseDto> CreateEventAsync(EventCreateDto eventCreateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> UpdateEventAsync(EventUpdateDto eventUpdateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> DeleteEventAsync(int id, CancellationToken cancellationToken);
    }
}
