using AutoMapper;
using EventsWeb.BusinessLogic.Exceptions;
using EventsWeb.BusinessLogic.Models;
using EventsWeb.BusinessLogic.Models.Events;
using EventsWeb.DataAccess;
using EventsWeb.Domain.Entities;

namespace EventsWeb.BusinessLogic.Services.Impl
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventResponseDto>> GetEventsAsync(CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.Events.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventResponseDto>>(events);
        }

        public async Task<IEnumerable<EventResponseDto>> GetEventsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.Events.GetByPageAsync(page, pageSize, cancellationToken);
            return _mapper.Map<IEnumerable<EventResponseDto>>(events);
        }

        public async Task<EventResponseDto> GetEventByIdAsync(int id, CancellationToken cancellationToken)
        {
            var ev = await _unitOfWork.Events.GetByIdAsync(id, cancellationToken);
            if (ev == null)
                throw new NotFoundException("Event not found");
            return _mapper.Map<EventResponseDto>(ev);
        }

        public async Task<EventCreateResponseDto> CreateEventAsync(EventCreateDto eventCreateDto,
            CancellationToken cancellationToken)
        {
            var ev = _mapper.Map<Event>(eventCreateDto);
            var createdEvent = await _unitOfWork.Events.AddAsync(ev, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EventCreateResponseDto>(createdEvent);
        }

        public async Task<BaseResponseDto> UpdateEventAsync(EventUpdateDto eventUpdateDto,
            CancellationToken cancellationToken)
        {
            var ev = await _unitOfWork.Events.GetByIdAsync(eventUpdateDto.Id, cancellationToken);
            if (ev == null)
                throw new NotFoundException("Event not found");
            _mapper.Map(eventUpdateDto, ev);
            var updatedEvent = _unitOfWork.Events.Update(ev);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = updatedEvent.Id };
        }

        public async Task<BaseResponseDto> DeleteEventAsync(int id, CancellationToken cancellationToken)
        {
            var ev = await _unitOfWork.Events.GetByIdAsync(id, cancellationToken);
            if (ev == null)
                throw new NotFoundException("Event not found");
            _unitOfWork.Events.Delete(ev);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = id };
        }
    }
}
