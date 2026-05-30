using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.Marcas;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/marcas")]
public class MarcasController : ControllerBase
{
    private readonly ISender _sender;
    public MarcasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllMarcasQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await _sender.Send(new GetMarcaByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Marcas item)
    {
        var result = await _sender.Send(new CreateMarcaCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.MarcaID, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(short id, [FromBody] Marcas item)
    {
        var result = await _sender.Send(new UpdateMarcaCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _sender.Send(new DeleteMarcaCommand(id));
        return result ? NoContent() : NotFound();
    }
}
