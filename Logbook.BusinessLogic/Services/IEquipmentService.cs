using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Equipments;

namespace Logbook.BusinessLogic.Services
{
    public interface IEquipmentService
    {
        public Task<IEnumerable<EquipmentResponseDto>> GetEquipmentsAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<EquipmentResponseDto>> GetEquipmentsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken);
        public Task<EquipmentResponseDto> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken);
        public Task<EquipmentCreateResponseDto> CreateEquipmentAsync(EquipmentCreateDto equipmentCreateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> UpdateEquipmentAsync(EquipmentUpdateDto equipmentUpdateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> DeleteEquipmentAsync(int id, CancellationToken cancellationToken);
    }
}
