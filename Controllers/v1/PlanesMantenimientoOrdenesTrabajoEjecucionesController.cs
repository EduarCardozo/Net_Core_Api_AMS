using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PlanesMantenimientoOrdenesTrabajoEjecuciones;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/planesmantenimientoordenestrabajoejecuciones")]
public class PlanesMantenimientoOrdenesTrabajoEjecucionesController : ControllerBase
{
    private readonly ISender _sender;
    public PlanesMantenimientoOrdenesTrabajoEjecucionesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPlanesMantenimientoOrdenesTrabajoEjecucionesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetPlanMantenimientoOrdenTrabajoEjecucionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlanesMantenimientoOrdenesTrabajoEjecuciones item)
    {
        var result = await _sender.Send(new CreatePlanMantenimientoOrdenTrabajoEjecucionCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.PlanMantenimientoOrdenTrabajoEjecucionID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PlanesMantenimientoOrdenesTrabajoEjecuciones item)
    {
        var result = await _sender.Send(new UpdatePlanMantenimientoOrdenTrabajoEjecucionCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeletePlanMantenimientoOrdenTrabajoEjecucionCommand(id));
        return result ? NoContent() : NotFound();
    }
}
