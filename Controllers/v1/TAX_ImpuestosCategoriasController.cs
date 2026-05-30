using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_ImpuestosCategorias;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_impuestoscategorias")]
public class TAX_ImpuestosCategoriasController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_ImpuestosCategoriasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_ImpuestosCategoriasQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetTAX_ImpuestoCategoriaByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_ImpuestosCategorias item)
    {
        var result = await _sender.Send(new CreateTAX_ImpuestoCategoriaCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.ImpuestoCategoriaID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TAX_ImpuestosCategorias item)
    {
        var result = await _sender.Send(new UpdateTAX_ImpuestoCategoriaCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteTAX_ImpuestoCategoriaCommand(id));
        return result ? NoContent() : NotFound();
    }
}
