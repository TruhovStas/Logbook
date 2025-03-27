using Logbook.Domain.Entities;

namespace Logbook.DataAccess.Repositories.Impl
{
    public class EquipmentRepository : BaseRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DatabaseContext context) : base(context) { }
    }
}
