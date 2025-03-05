using EventsWeb.DataAccess.Repositories.Impl;

namespace EventsWeb.DataAccess
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken);
        public EventRepository Events { get; }
        public ParticipantRepository Participants { get; }
    }
}

