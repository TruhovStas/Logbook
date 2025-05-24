using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Substances;

namespace Logbook.BusinessLogic.Services
{
    public interface ISubstanceService
    {
        public Task<IEnumerable<SubstanceResponseDto>> GetSubstancesAsync(CancellationToken cancellationToken);
        public Task<PaginatedResult<SubstanceResponseDto>> GetSubstancesByPageAsync(int page, int pageSize,
            string sortColumn, string sortDirection, CancellationToken cancellationToken);
        public Task<SubstanceResponseDto> GetSubstanceByIdAsync(int id, CancellationToken cancellationToken);
        public Task<SubstanceCreateResponseDto> CreateSubstanceAsync(SubstanceCreateDto solutionCreateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> UpdateSubstanceAsync(SubstanceUpdateDto solutionUpdateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> DeleteSubstanceAsync(int id, CancellationToken cancellationToken);
    }
}
