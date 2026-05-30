using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_DocumentosTalonariosTipos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_documentostalonariotipos")]
public class TAX_DocumentosTalonariosTiposController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_DocumentosTalonariosTiposController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_DocumentosTalonariosTiposQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetTAX_DocumentoTalonarioTipoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_DocumentosTalonariosTipos item)
    {
        var result = await _sender.Send(new CreateTAX_DocumentoTalonarioTipoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.DocumentoTalonarioTipoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TAX_DocumentosTalonariosTipos item)
    {
        var result = await _sender.Send(new UpdateTAX_DocumentoTalonarioTipoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteTAX_DocumentoTalonarioTipoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
