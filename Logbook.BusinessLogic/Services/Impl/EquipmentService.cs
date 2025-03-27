using AutoMapper;
using Logbook.BusinessLogic.Exceptions;
using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Equipments;
using Logbook.DataAccess;
using Logbook.Domain.Entities;

namespace Logbook.BusinessLogic.Services.Impl
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EquipmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EquipmentResponseDto>> GetEquipmentsAsync(CancellationToken cancellationToken)
        {
            var equipments = await _unitOfWork.Equipments.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EquipmentResponseDto>>(equipments);
        }

        public async Task<IEnumerable<EquipmentResponseDto>> GetEquipmentsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            var equipments = await _unitOfWork.Equipments.GetByPageAsync(page, pageSize, cancellationToken);
            return _mapper.Map<IEnumerable<EquipmentResponseDto>>(equipments);
        }

        public async Task<EquipmentResponseDto> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken)
        {
            var equipment = await _unitOfWork.Equipments.GetByIdAsync(id, cancellationToken);
            if (equipment == null)
                throw new NotFoundException("Оборудование не найдено.");
            return _mapper.Map<EquipmentResponseDto>(equipment);
        }

        public async Task<EquipmentCreateResponseDto> CreateEquipmentAsync(EquipmentCreateDto equipmentCreateDto,
            CancellationToken cancellationToken)
        {
            var equipment = _mapper.Map<Equipment>(equipmentCreateDto);
            var createdEquipment = await _unitOfWork.Equipments.AddAsync(equipment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EquipmentCreateResponseDto>(createdEquipment);
        }

        public async Task<BaseResponseDto> UpdateEquipmentAsync(EquipmentUpdateDto equipmentUpdateDto,
            CancellationToken cancellationToken)
        {
            var equipment = await _unitOfWork.Equipments.GetByIdAsync(equipmentUpdateDto.Id, cancellationToken);
            if (equipment == null)
                throw new NotFoundException("Оборудование не найдено.");
            _mapper.Map(equipmentUpdateDto, equipment);
            var updatedEquipment = _unitOfWork.Equipments.Update(equipment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = updatedEquipment.Id };
        }

        public async Task<BaseResponseDto> DeleteEquipmentAsync(int id, CancellationToken cancellationToken)
        {
            var equipment = await _unitOfWork.Equipments.GetByIdAsync(id, cancellationToken);
            if (equipment == null)
                throw new NotFoundException("Equipment not found");
            _unitOfWork.Equipments.Delete(equipment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = id };
        }
    }
}
