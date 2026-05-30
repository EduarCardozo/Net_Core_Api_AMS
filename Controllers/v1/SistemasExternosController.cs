using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.SistemasExternos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/sistemasexternos")]
public class SistemasExternosController : ControllerBase
{
    private readonly ISender _sender;
    public SistemasExternosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllSistemasExternosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await _sender.Send(new GetSistemaExternoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SistemasExternos item)
    {
        var result = await _sender.Send(new CreateSistemaExternoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.SistemaExternoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(short id, [FromBody] SistemasExternos item)
    {
        var result = await _sender.Send(new UpdateSistemaExternoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _sender.Send(new DeleteSistemaExternoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
