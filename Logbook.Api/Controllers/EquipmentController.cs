using Logbook.BusinessLogic.Models;
using Logbook.BusinessLogic.Models.Equipments;
using Logbook.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logbook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentResponseDto>>> GetEquipment(CancellationToken cancellationToken)
        {
            return Ok(await _equipmentService.GetEquipmentsAsync(cancellationToken));
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<EquipmentResponseDto>>> GetEquipmentByPage(int page, int pageSize,
        CancellationToken cancellationToken)
        {
            return Ok(await _equipmentService.GetEquipmentsByPageAsync(page, pageSize, cancellationToken));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentResponseDto>> GetEquipmentById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _equipmentService.GetEquipmentByIdAsync(id, cancellationToken));
        }

        //[Authorize(Policy = "AuthenticatedUser")]
        [HttpPost]
        public async Task<ActionResult<EquipmentCreateResponseDto>> CreateEquipment([FromForm] EquipmentCreateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _equipmentService.CreateEquipmentAsync(ev, cancellationToken));
        }

        //[Authorize(Policy = "AuthenticatedUser")]
        [HttpPut]
        public async Task<ActionResult<BaseResponseDto>> UpdateEquipment([FromForm] EquipmentUpdateDto ev,
            CancellationToken cancellationToken)
        {
            return Ok(await _equipmentService.UpdateEquipmentAsync(ev, cancellationToken));
        }

        //[Authorize(Policy = "AuthenticatedUser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteEquipment(int id, CancellationToken cancellationToken)
        {
            return Ok(await _equipmentService.DeleteEquipmentAsync(id, cancellationToken));
        }
    }
}
