using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.Roles;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/roles")]
public class RolesController : ControllerBase
{
    private readonly ISender _sender;
    public RolesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRolesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await _sender.Send(new GetRolByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Roles item)
    {
        var result = await _sender.Send(new CreateRolCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.Id, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(short id, [FromBody] Roles item)
    {
        var result = await _sender.Send(new UpdateRolCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _sender.Send(new DeleteRolCommand(id));
        return result ? NoContent() : NotFound();
    }
}
