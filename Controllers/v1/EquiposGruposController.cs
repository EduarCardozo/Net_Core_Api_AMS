using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.EquiposGrupos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/equiposgrupos")]
public class EquiposGruposController : ControllerBase
{
    private readonly ISender _sender;
    public EquiposGruposController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllEquiposGruposQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await _sender.Send(new GetEquipoGrupoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EquiposGrupos item)
    {
        var result = await _sender.Send(new CreateEquipoGrupoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.EquipoGrupoID, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(short id, [FromBody] EquiposGrupos item)
    {
        var result = await _sender.Send(new UpdateEquipoGrupoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _sender.Send(new DeleteEquipoGrupoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
