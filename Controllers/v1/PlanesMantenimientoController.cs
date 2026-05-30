using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PlanesMantenimiento;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/planesmantenimiento")]
public class PlanesMantenimientoController : ControllerBase
{
    private readonly ISender _sender;
    public PlanesMantenimientoController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPlanesMantenimientoQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetPlanMantenimientoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlanesMantenimiento item)
    {
        var result = await _sender.Send(new CreatePlanMantenimientoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.PlanMantenimientoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PlanesMantenimiento item)
    {
        var result = await _sender.Send(new UpdatePlanMantenimientoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeletePlanMantenimientoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
