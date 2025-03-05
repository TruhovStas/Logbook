using EventsWeb.BusinessLogic.Models;
using EventsWeb.BusinessLogic.Models.Events;
using EventsWeb.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponseDto>>> GetEvents(CancellationToken cancellationToken)
        {
            return Ok(await _eventService.GetEventsAsync(cancellationToken));
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<EventResponseDto>>> GetEventsByPage(int page, int pageSize,
        CancellationToken cancellationToken)
        {
            return Ok(await _eventService.GetEventsByPageAsync(page, pageSize, cancellationToken));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponseDto>> GetEventById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _eventService.GetEventByIdAsync(id, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPost]
        public async Task<ActionResult<EventCreateResponseDto>> CreateEvent([FromForm] EventCreateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _eventService.CreateEventAsync(ev, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPut]
        public async Task<ActionResult<BaseResponseDto>> UpdateEvent([FromForm] EventUpdateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _eventService.UpdateEventAsync(ev, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteEvent(int id, CancellationToken cancellationToken)
        {
            return Ok(await _eventService.DeleteEventAsync(id, cancellationToken));
        }
    }
}
