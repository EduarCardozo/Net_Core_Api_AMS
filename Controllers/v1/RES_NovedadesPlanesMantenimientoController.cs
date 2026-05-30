using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RES_NovedadesPlanesMantenimiento;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/res_novedadesplanesmantenimiento")]
public class RES_NovedadesPlanesMantenimientoController : ControllerBase
{
    private readonly ISender _sender;
    public RES_NovedadesPlanesMantenimientoController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRES_NovedadesPlanesMantenimientoQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetRES_NovedadPlanMantenimientoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RES_NovedadesPlanesMantenimiento item)
    {
        var result = await _sender.Send(new CreateRES_NovedadPlanMantenimientoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.NovedadPlanMantenimientoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RES_NovedadesPlanesMantenimiento item)
    {
        var result = await _sender.Send(new UpdateRES_NovedadPlanMantenimientoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteRES_NovedadPlanMantenimientoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
