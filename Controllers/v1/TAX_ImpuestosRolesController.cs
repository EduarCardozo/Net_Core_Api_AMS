using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_ImpuestosRoles;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_impuestosroles")]
public class TAX_ImpuestosRolesController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_ImpuestosRolesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_ImpuestosRolesQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(byte id)
    {
        var result = await _sender.Send(new GetTAX_ImpuestoRolByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_ImpuestosRoles item)
    {
        var result = await _sender.Send(new CreateTAX_ImpuestoRolCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.ImpuestoRolID, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(byte id, [FromBody] TAX_ImpuestosRoles item)
    {
        var result = await _sender.Send(new UpdateTAX_ImpuestoRolCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(byte id)
    {
        var result = await _sender.Send(new DeleteTAX_ImpuestoRolCommand(id));
        return result ? NoContent() : NotFound();
    }
}
