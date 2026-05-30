using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RES_TurnosRecursos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/res_turnosrecursos")]
public class RES_TurnosRecursosController : ControllerBase
{
    private readonly ISender _sender;
    public RES_TurnosRecursosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRES_TurnosRecursosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetRES_TurnoRecursoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RES_TurnosRecursos item)
    {
        var result = await _sender.Send(new CreateRES_TurnoRecursoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.TurnoRecursoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RES_TurnosRecursos item)
    {
        var result = await _sender.Send(new UpdateRES_TurnoRecursoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteRES_TurnoRecursoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
