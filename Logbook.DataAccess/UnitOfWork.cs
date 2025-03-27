using Logbook.DataAccess.Repositories.Impl;

namespace Logbook.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _dbContext;
        private EquipmentRepository _equipmentRepository;
        private ForecastRepository _forecastRepository { get; set; }
        private SolutionRepository _solutionRepository;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EquipmentRepository Equipments
        {
            get
            {
                if (_equipmentRepository == null)
                    _equipmentRepository = new EquipmentRepository(_dbContext);
                return _equipmentRepository;
            }
        }

        public ForecastRepository Forecasts
        {
            get
            {
                if (_forecastRepository == null)
                    _forecastRepository = new ForecastRepository(_dbContext);
                return _forecastRepository;
            }
        }
        public SolutionRepository Solutions
        {
            get
            {
                if (_solutionRepository == null)
                    _solutionRepository = new SolutionRepository(_dbContext);
                return _solutionRepository;
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
