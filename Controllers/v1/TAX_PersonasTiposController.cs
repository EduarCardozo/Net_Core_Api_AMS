using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_PersonasTipos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_personastipos")]
public class TAX_PersonasTiposController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_PersonasTiposController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_PersonasTiposQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(byte id)
    {
        var result = await _sender.Send(new GetTAX_PersonaTipoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_PersonasTipos item)
    {
        var result = await _sender.Send(new CreateTAX_PersonaTipoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.PersonaTipoID, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(byte id, [FromBody] TAX_PersonasTipos item)
    {
        var result = await _sender.Send(new UpdateTAX_PersonaTipoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(byte id)
    {
        var result = await _sender.Send(new DeleteTAX_PersonaTipoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
