using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.G_Direcciones;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/g_direcciones")]
public class G_DireccionesController : ControllerBase
{
    private readonly ISender _sender;
    public G_DireccionesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllG_DireccionesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetG_DireccionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] G_Direcciones item)
    {
        var result = await _sender.Send(new CreateG_DireccionCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.DireccionID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] G_Direcciones item)
    {
        var result = await _sender.Send(new UpdateG_DireccionCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteG_DireccionCommand(id));
        return result ? NoContent() : NotFound();
    }
}
