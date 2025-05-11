using AutoMapper;
using Logbook.BusinessLogic.Exceptions;
using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Solutions;
using Logbook.DataAccess;
using Logbook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Logbook.BusinessLogic.Services.Impl
{
    public class SolutionService : ISolutionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SolutionService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<SolutionResponseDto>> GetSolutionsAsync(CancellationToken cancellationToken)
        {
            var solutions = await _unitOfWork.Solutions.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<SolutionResponseDto>>(solutions);
        }

        public async Task<PaginatedResult<SolutionResponseDto>> GetSolutionsByPageAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var allSolution = await _unitOfWork.Solutions.GetAllAsync(cancellationToken);
            var solutions = await _unitOfWork.Solutions.GetByPageAsync(page, pageSize, cancellationToken);
            var items = _mapper.Map<IEnumerable<SolutionResponseDto>>(solutions);

            var totalPages = (int)Math.Ceiling((double)allSolution.Count() / pageSize);

            return new PaginatedResult<SolutionResponseDto>
            {
                Items = items,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }

        public async Task<SolutionResponseDto> GetSolutionByIdAsync(int id, CancellationToken cancellationToken)
        {
            var solution = await _unitOfWork.Solutions.GetByIdAsync(id, cancellationToken);
            if (solution == null)
                throw new NotFoundException("Оборудование не найдено.");
            return _mapper.Map<SolutionResponseDto>(solution);
        }

        public async Task<SolutionCreateResponseDto> CreateSolutionAsync(SolutionCreateDto solutionCreateDto,
            CancellationToken cancellationToken)
        {
            var solution = _mapper.Map<Solution>(solutionCreateDto);
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                throw new UnauthorizedAccessException("Не удалось получить имя пользователя из токена.");
            }
            solution.Login = userName;
            List<double> K = new List<double>();
            double avg = 0;
            for (int i = 0; i < solution.SubstanceMasses.Count(); i++)
            {
                K.Add(solution.SubstanceMasses[i] * 1000 /
                    solution.SubstanceMolarMass /
                    solution.SubstanceConcentration /
                    solution.SubstanceVolumes[i]);
                avg += K[i];
            }
            solution.AvgCorrectionFactor = avg / solution.SubstanceMasses.Count();
            solution.CorrectionFactors = K;
            var createdSolution = await _unitOfWork.Solutions.AddAsync(solution, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SolutionCreateResponseDto>(createdSolution);
        }

        public async Task<BaseResponseDto> UpdateSolutionAsync(SolutionUpdateDto solutionUpdateDto,
            CancellationToken cancellationToken)
        {
            var solution = await _unitOfWork.Solutions.GetByIdAsync(solutionUpdateDto.Id, cancellationToken);
            if (solution == null)
                throw new NotFoundException("Оборудование не найдено.");
            _mapper.Map(solutionUpdateDto, solution);
            var updatedSolution = _unitOfWork.Solutions.Update(solution);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = updatedSolution.Id };
        }

        public async Task<BaseResponseDto> DeleteSolutionAsync(int id, CancellationToken cancellationToken)
        {
            var solution = await _unitOfWork.Solutions.GetByIdAsync(id, cancellationToken);
            if (solution == null)
                throw new NotFoundException("Solution not found");
            _unitOfWork.Solutions.Delete(solution);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = id };
        }
    }
}
