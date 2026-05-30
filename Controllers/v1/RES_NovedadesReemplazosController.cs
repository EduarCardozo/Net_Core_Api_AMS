using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RES_NovedadesReemplazos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/res_novedadesreemplazos")]
public class RES_NovedadesReemplazosController : ControllerBase
{
    private readonly ISender _sender;
    public RES_NovedadesReemplazosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRES_NovedadesReemplazosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetRES_NovedadReemplazoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RES_NovedadesReemplazos item)
    {
        var result = await _sender.Send(new CreateRES_NovedadReemplazoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.NovedadReemplazoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RES_NovedadesReemplazos item)
    {
        var result = await _sender.Send(new UpdateRES_NovedadReemplazoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteRES_NovedadReemplazoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
