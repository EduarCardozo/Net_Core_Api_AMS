using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_Impuestos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_impuestos")]
public class TAX_ImpuestosController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_ImpuestosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_ImpuestosQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await _sender.Send(new GetTAX_ImpuestoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_Impuestos item)
    {
        var result = await _sender.Send(new CreateTAX_ImpuestoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.ImpuestoID, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(short id, [FromBody] TAX_Impuestos item)
    {
        var result = await _sender.Send(new UpdateTAX_ImpuestoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _sender.Send(new DeleteTAX_ImpuestoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
