using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Solutions;

namespace Logbook.BusinessLogic.Services
{
    public interface ISolutionService
    {
        public Task<IEnumerable<SolutionResponseDto>> GetSolutionsAsync(CancellationToken cancellationToken);
        public Task<PaginatedResult<SolutionResponseDto>> GetSolutionsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken);
        public Task<SolutionResponseDto> GetSolutionByIdAsync(int id, CancellationToken cancellationToken);
        public Task<SolutionCreateResponseDto> CreateSolutionAsync(SolutionCreateDto solutionCreateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> UpdateSolutionAsync(SolutionUpdateDto solutionUpdateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> DeleteSolutionAsync(int id, CancellationToken cancellationToken);
    }
}
