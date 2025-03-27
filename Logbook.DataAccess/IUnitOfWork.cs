using Logbook.DataAccess.Repositories.Impl;

namespace Logbook.DataAccess
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken);
        public EquipmentRepository Equipments { get; }
        public ForecastRepository Forecasts { get; }
        public SolutionRepository Solutions { get; }
    }
}

