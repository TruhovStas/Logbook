using Logbook.Domain.Entities;

namespace Logbook.DataAccess.Repositories.Impl
{
    public class SubstanceRepository : BaseRepository<Substance>, ISubstanceRepository
    {
        public SubstanceRepository(DatabaseContext context) : base(context) { }
    }
}
