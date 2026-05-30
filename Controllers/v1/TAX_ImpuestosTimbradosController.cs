using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_ImpuestosTimbrados;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_impuestostimbrados")]
public class TAX_ImpuestosTimbradosController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_ImpuestosTimbradosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_ImpuestosTimbradosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetTAX_ImpuestoTimbradoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_ImpuestosTimbrados item)
    {
        var result = await _sender.Send(new CreateTAX_ImpuestoTimbradoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.ImpuestoTimbradoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TAX_ImpuestosTimbrados item)
    {
        var result = await _sender.Send(new UpdateTAX_ImpuestoTimbradoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteTAX_ImpuestoTimbradoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
