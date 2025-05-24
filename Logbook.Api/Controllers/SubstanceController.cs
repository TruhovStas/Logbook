using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Substances;
using Logbook.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logbook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubstanceController : ControllerBase
    {
        private readonly ISubstanceService _substanceService;

        public SubstanceController(ISubstanceService substanceService)
        {
            _substanceService = substanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubstanceResponseDto>>> GetSubstance(CancellationToken cancellationToken)
        {
            return Ok(await _substanceService.GetSubstancesAsync(cancellationToken));
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResult<SubstanceResponseDto>>> GetSubstanceByPage(
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10, string sortColumn = "PreparationDate",
            string sortDirection = "asc",CancellationToken cancellationToken = default)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page and pageSize must be greater than 0.");
            return Ok(await _substanceService.GetSubstancesByPageAsync(page, pageSize, sortColumn, sortDirection, cancellationToken));
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<SubstanceResponseDto>> GetSubstanceById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _substanceService.GetSubstanceByIdAsync(id, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPost]
        public async Task<ActionResult<SubstanceCreateResponseDto>> CreateSubstance([FromBody] SubstanceCreateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _substanceService.CreateSubstanceAsync(ev, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPut]
        public async Task<ActionResult<BaseResponseDto>> UpdateSubstance([FromBody] SubstanceUpdateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _substanceService.UpdateSubstanceAsync(ev, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteSubstance(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _substanceService.DeleteSubstanceAsync(id, cancellationToken));
            }
            catch
            {
                return Conflict("Это вещество использовалось для контроля");
            }
        }
    }
}
