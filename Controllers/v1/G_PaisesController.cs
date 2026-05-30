using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.G_Paises;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/g_paises")]
public class G_PaisesController : ControllerBase
{
    private readonly ISender _sender;
    public G_PaisesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllG_PaisesQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await _sender.Send(new GetG_PaisByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] G_Paises item)
    {
        var result = await _sender.Send(new CreateG_PaisCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.PaisID, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(short id, [FromBody] G_Paises item)
    {
        var result = await _sender.Send(new UpdateG_PaisCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _sender.Send(new DeleteG_PaisCommand(id));
        return result ? NoContent() : NotFound();
    }
}
