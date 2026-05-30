using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RES_TurnosLocaciones;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/res_turnoslocaciones")]
public class RES_TurnosLocacionesController : ControllerBase
{
    private readonly ISender _sender;
    public RES_TurnosLocacionesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRES_TurnosLocacionesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetRES_TurnoLocacionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RES_TurnosLocaciones item)
    {
        var result = await _sender.Send(new CreateRES_TurnoLocacionCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.TurnoLocacionID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RES_TurnosLocaciones item)
    {
        var result = await _sender.Send(new UpdateRES_TurnoLocacionCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteRES_TurnoLocacionCommand(id));
        return result ? NoContent() : NotFound();
    }
}
