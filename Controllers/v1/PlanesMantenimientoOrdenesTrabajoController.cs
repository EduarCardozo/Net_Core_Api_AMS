using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PlanesMantenimientoOrdenesTrabajo;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/planesmantenimientoordenestrabajo")]
public class PlanesMantenimientoOrdenesTrabajoController : ControllerBase
{
    private readonly ISender _sender;
    public PlanesMantenimientoOrdenesTrabajoController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPlanesMantenimientoOrdenesTrabajoQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetPlanMantenimientoOrdenTrabajoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlanesMantenimientoOrdenesTrabajo item)
    {
        var result = await _sender.Send(new CreatePlanMantenimientoOrdenTrabajoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.PlanMantenimientoOrdenTrabajoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PlanesMantenimientoOrdenesTrabajo item)
    {
        var result = await _sender.Send(new UpdatePlanMantenimientoOrdenTrabajoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeletePlanMantenimientoOrdenTrabajoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
