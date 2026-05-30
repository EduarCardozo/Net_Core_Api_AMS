using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RES_NovedadesTipos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/res_novedadestipos")]
public class RES_NovedadesTiposController : ControllerBase
{
    private readonly ISender _sender;
    public RES_NovedadesTiposController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRES_NovedadesTiposQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetRES_NovedadTipoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RES_NovedadesTipos item)
    {
        var result = await _sender.Send(new CreateRES_NovedadTipoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.NovedadTipoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RES_NovedadesTipos item)
    {
        var result = await _sender.Send(new UpdateRES_NovedadTipoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteRES_NovedadTipoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
