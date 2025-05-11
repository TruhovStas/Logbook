using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Solutions;
using Logbook.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logbook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService _solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolutionResponseDto>>> GetSolution(CancellationToken cancellationToken)
        {
            return Ok(await _solutionService.GetSolutionsAsync(cancellationToken));
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResult<SolutionResponseDto>>> GetSolutionByPage(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page and pageSize must be greater than 0.");
            return Ok(await _solutionService.GetSolutionsByPageAsync(page, pageSize, cancellationToken));
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<SolutionResponseDto>> GetSolutionById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _solutionService.GetSolutionByIdAsync(id, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPost]
        public async Task<ActionResult<SolutionCreateResponseDto>> CreateSolution([FromBody] SolutionCreateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _solutionService.CreateSolutionAsync(ev, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPut]
        public async Task<ActionResult<BaseResponseDto>> UpdateSolution([FromBody] SolutionUpdateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _solutionService.UpdateSolutionAsync(ev, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteSolution(int id, CancellationToken cancellationToken)
        {
            return Ok(await _solutionService.DeleteSolutionAsync(id, cancellationToken));
        }
    }
}
