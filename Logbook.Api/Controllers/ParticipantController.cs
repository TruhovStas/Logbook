using EventsWeb.BusinessLogic.Models.Participants;
using EventsWeb.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWeb.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet("event/{eventId:int}")]
        public async Task<ActionResult<IEnumerable<ParticipantResponseDto>>> GetParticipantsByEvent(int eventId, CancellationToken cancellationToken)
        {
            return Ok(await _participantService.GetParticipantsByEventAsync(eventId, cancellationToken));
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<ParticipantResponseDto>>> GetParticipantsByPage(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            return Ok(await _participantService.GetParticipantsByPageAsync(page, pageSize, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ParticipantResponseDto>> GetParticipantById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _participantService.GetParticipantByIdAsync(id, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPost]
        public async Task<ActionResult<ParticipantCreateResponseDto>> CreateParticipant([FromForm] ParticipantCreateDto participantCreateDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _participantService.CreateParticipantAsync(participantCreateDto, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ParticipantResponseDto>> DeleteParticipant(int id, CancellationToken cancellationToken)
        {
            return Ok(await _participantService.DeleteParticipantAsync(id, cancellationToken));
        }
    }
}
