using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.EquiposMediciones;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/equiposmediciones")]
public class EquiposMedicionesController : ControllerBase
{
    private readonly ISender _sender;
    public EquiposMedicionesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllEquiposMedicionesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetEquipoMedicionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EquiposMediciones item)
    {
        var result = await _sender.Send(new CreateEquipoMedicionCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.EquipoMedicionID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] EquiposMediciones item)
    {
        var result = await _sender.Send(new UpdateEquipoMedicionCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteEquipoMedicionCommand(id));
        return result ? NoContent() : NotFound();
    }
}
