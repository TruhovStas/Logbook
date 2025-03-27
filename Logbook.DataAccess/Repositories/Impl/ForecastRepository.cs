using Logbook.Domain.Entities;

namespace Logbook.DataAccess.Repositories.Impl
{
    public class ForecastRepository : BaseRepository<Forecast>, IForecastRepository
    {
        public ForecastRepository(DatabaseContext context) : base(context) { }
    }
}
