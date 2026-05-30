using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.DocumentosElectronicos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/documentoselectronicos")]
public class DocumentosElectronicosController : ControllerBase
{
    private readonly ISender _sender;
    public DocumentosElectronicosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllDocumentosElectronicosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetDocumentoElectronicoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DocumentosElectronicos item)
    {
        var result = await _sender.Send(new CreateDocumentoElectronicoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.DocumentoElectronicoId, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] DocumentosElectronicos item)
    {
        var result = await _sender.Send(new UpdateDocumentoElectronicoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteDocumentoElectronicoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
