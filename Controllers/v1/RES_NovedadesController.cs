using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RES_Novedades;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/res_novedades")]
public class RES_NovedadesController : ControllerBase
{
    private readonly ISender _sender;
    public RES_NovedadesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRES_NovedadesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetRES_NovedadByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RES_Novedades item)
    {
        var result = await _sender.Send(new CreateRES_NovedadCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.NovedadID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RES_Novedades item)
    {
        var result = await _sender.Send(new UpdateRES_NovedadCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteRES_NovedadCommand(id));
        return result ? NoContent() : NotFound();
    }
}
