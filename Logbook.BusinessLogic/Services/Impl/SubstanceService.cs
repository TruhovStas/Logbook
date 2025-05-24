using AutoMapper;
using Logbook.BusinessLogic.Exceptions;
using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Substances;
using Logbook.DataAccess;
using Logbook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Logbook.BusinessLogic.Services.Impl
{
    public class SubstanceService : ISubstanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubstanceService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<SubstanceResponseDto>> GetSubstancesAsync(CancellationToken cancellationToken)
        {
            var substances = await _unitOfWork.Substances.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<SubstanceResponseDto>>(substances);
        }

        public async Task<PaginatedResult<SubstanceResponseDto>> GetSubstancesByPageAsync(int page, int pageSize,
            string sortColumn, string sortDirection, CancellationToken cancellationToken)
        {
            var allSubstance = await _unitOfWork.Substances.GetAllAsync(cancellationToken);
            var substances = await _unitOfWork.Substances.GetByPageAsync(page, pageSize, cancellationToken);
            var items = _mapper.Map<IEnumerable<SubstanceResponseDto>>(substances).OrderBy(s => s.Formula);

            var totalPages = (int)Math.Ceiling((double)allSubstance.Count() / pageSize);

            return new PaginatedResult<SubstanceResponseDto>
            {
                Items = items,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }

        public async Task<SubstanceResponseDto> GetSubstanceByIdAsync(int id, CancellationToken cancellationToken)
        {
            var substance = await _unitOfWork.Substances.GetByIdAsync(id, cancellationToken);
            if (substance == null)
                throw new NotFoundException("Установочное вещество не найдено.");
            return _mapper.Map<SubstanceResponseDto>(substance);
        }

        public async Task<SubstanceCreateResponseDto> CreateSubstanceAsync(SubstanceCreateDto substanceCreateDto,
            CancellationToken cancellationToken)
        {
            var substance = _mapper.Map<Substance>(substanceCreateDto);
            var createdSubstance = await _unitOfWork.Substances.AddAsync(substance, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SubstanceCreateResponseDto>(createdSubstance);
        }

        public async Task<BaseResponseDto> UpdateSubstanceAsync(SubstanceUpdateDto substanceUpdateDto,
            CancellationToken cancellationToken)
        {
            var substance = await _unitOfWork.Substances.GetByIdAsync(substanceUpdateDto.Id, cancellationToken);
            if (substance == null)
                throw new NotFoundException("Установочное вещество не найдено.");
            _mapper.Map(substanceUpdateDto, substance);
            var updatedSubstance = _unitOfWork.Substances.Update(substance);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = updatedSubstance.Id };
        }

        public async Task<BaseResponseDto> DeleteSubstanceAsync(int id, CancellationToken cancellationToken)
        {
            var substance = await _unitOfWork.Substances.GetByIdAsync(id, cancellationToken);
            var s = await _unitOfWork.Solutions.GetFirstByPredicateAsync(s => s.Substance == substance);
            if (s != null)
            {
                throw new ArgumentException();
            }
            if (substance == null)
                throw new NotFoundException("Установочное вещество не найдено.");
            _unitOfWork.Substances.Delete(substance);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = id };
        }
    }
}
