using EventsWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsWeb.DataAccess.Repositories.Impl
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(DatabaseContext context) : base(context) { }
        public async Task<IEnumerable<Participant>?> GetListByEventAsync(int eventId, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(p => p.Event.Id == eventId).ToListAsync(cancellationToken);
        }
    }
}
