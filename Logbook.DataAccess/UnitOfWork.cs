using EventsWeb.DataAccess.Repositories.Impl;

namespace EventsWeb.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _dbContext;
        private EventRepository _eventRepository;
        private ParticipantRepository _participantRepository;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EventRepository Events
        {
            get
            {
                if (_eventRepository == null)
                    _eventRepository = new EventRepository(_dbContext);
                return _eventRepository;
            }
        }

        public ParticipantRepository Participants
        {
            get
            {
                if (_participantRepository == null)
                    _participantRepository = new ParticipantRepository(_dbContext);
                return _participantRepository;
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
