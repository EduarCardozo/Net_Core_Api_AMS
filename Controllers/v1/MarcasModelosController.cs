using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.MarcasModelos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/marcasmodelos")]
public class MarcasModelosController : ControllerBase
{
    private readonly ISender _sender;
    public MarcasModelosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllMarcasModelosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetMarcaModeloByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MarcasModelos item)
    {
        var result = await _sender.Send(new CreateMarcaModeloCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.MarcaModeloID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] MarcasModelos item)
    {
        var result = await _sender.Send(new UpdateMarcaModeloCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteMarcaModeloCommand(id));
        return result ? NoContent() : NotFound();
    }
}
