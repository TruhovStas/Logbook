using Logbook.Domain.Entities;

namespace Logbook.DataAccess.Repositories.Impl
{
    public class SolutionRepository : BaseRepository<Solution>, ISolutionRepository
    {
        public SolutionRepository(DatabaseContext context) : base(context) { }
    }
}
