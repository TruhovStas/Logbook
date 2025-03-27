using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Forecasts;
using Logbook.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logbook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IForecastService _forecastService;

        public ForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForecastResponseDto>>> GetForecast(CancellationToken cancellationToken)
        {
            return Ok(await _forecastService.GetForecastsAsync(cancellationToken));
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<ForecastResponseDto>>> GetForecastByPage(int page, int pageSize,
        CancellationToken cancellationToken)
        {
            return Ok(await _forecastService.GetForecastsByPageAsync(page, pageSize, cancellationToken));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ForecastResponseDto>> GetForecastById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _forecastService.GetForecastByIdAsync(id, cancellationToken));
        }

        //[Authorize(Policy = "AuthenticatedUser")]
        [HttpPost]
        public async Task<ActionResult<ForecastCreateResponseDto>> CreateForecast([FromForm] ForecastCreateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _forecastService.CreateForecastAsync(ev, cancellationToken));
        }

        //[Authorize(Policy = "AuthenticatedUser")]
        [HttpPut]
        public async Task<ActionResult<BaseResponseDto>> UpdateForecast([FromForm] ForecastUpdateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _forecastService.UpdateForecastAsync(ev, cancellationToken));
        }

        //[Authorize(Policy = "AuthenticatedUser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteForecast(int id, CancellationToken cancellationToken)
        {
            return Ok(await _forecastService.DeleteForecastAsync(id, cancellationToken));
        }
    }
}
