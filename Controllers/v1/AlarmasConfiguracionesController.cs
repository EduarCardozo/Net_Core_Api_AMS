using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.AlarmasConfiguraciones;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/alarmasconfiguraciones")]
public class AlarmasConfiguracionesController : ControllerBase
{
    private readonly ISender _sender;
    public AlarmasConfiguracionesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllAlarmasConfiguracionesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetAlarmaConfiguracionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AlarmasConfiguraciones item)
    {
        var result = await _sender.Send(new CreateAlarmaConfiguracionCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.AlarmaConfiguracionID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AlarmasConfiguraciones item)
    {
        var result = await _sender.Send(new UpdateAlarmaConfiguracionCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteAlarmaConfiguracionCommand(id));
        return result ? NoContent() : NotFound();
    }
}
