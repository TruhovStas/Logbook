using EventsWeb.BusinessLogic.Models;
using EventsWeb.BusinessLogic.Models.Participants;

namespace EventsWeb.BusinessLogic.Services
{
    public interface IParticipantService
    {
        public Task<IEnumerable<ParticipantResponseDto>> GetParticipantsByEventAsync(int eventId, CancellationToken cancellationToken);
        public Task<IEnumerable<ParticipantResponseDto>> GetParticipantsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken);
        public Task<ParticipantResponseDto> GetParticipantByIdAsync(int id, CancellationToken cancellationToken);
        public Task<ParticipantCreateResponseDto> CreateParticipantAsync(ParticipantCreateDto participantCreateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> DeleteParticipantAsync(int id, CancellationToken cancellationToken);
    }
}
