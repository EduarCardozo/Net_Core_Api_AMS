using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.G_SubRegiones;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/g_subregiones")]
public class G_SubRegionesController : ControllerBase
{
    private readonly ISender _sender;
    public G_SubRegionesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllG_SubRegionesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetG_SubRegionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] G_SubRegiones item)
    {
        var result = await _sender.Send(new CreateG_SubRegionCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.SubRegionID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] G_SubRegiones item)
    {
        var result = await _sender.Send(new UpdateG_SubRegionCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteG_SubRegionCommand(id));
        return result ? NoContent() : NotFound();
    }
}
