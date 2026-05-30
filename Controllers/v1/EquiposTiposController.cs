using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.EquiposTipos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/equipostipos")]
public class EquiposTiposController : ControllerBase
{
    private readonly ISender _sender;
    public EquiposTiposController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllEquiposTiposQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(byte id)
    {
        var result = await _sender.Send(new GetEquipoTipoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EquiposTipos item)
    {
        var result = await _sender.Send(new CreateEquipoTipoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.EquipoTipoID, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(byte id, [FromBody] EquiposTipos item)
    {
        var result = await _sender.Send(new UpdateEquipoTipoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(byte id)
    {
        var result = await _sender.Send(new DeleteEquipoTipoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
