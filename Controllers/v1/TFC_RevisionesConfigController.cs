using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TFC_RevisionesConfig;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tfc_revisionesconfig")]
public class TFC_RevisionesConfigController : ControllerBase
{
    private readonly ISender _sender;
    public TFC_RevisionesConfigController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTFC_RevisionesConfigQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetTFC_RevisionConfigByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TFC_RevisionesConfig item)
    {
        var result = await _sender.Send(new CreateTFC_RevisionConfigCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.RevisionConfigID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TFC_RevisionesConfig item)
    {
        var result = await _sender.Send(new UpdateTFC_RevisionConfigCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteTFC_RevisionConfigCommand(id));
        return result ? NoContent() : NotFound();
    }
}
