using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PartesCategorias;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/partescategorias")]
public class PartesCategoriasController : ControllerBase
{
    private readonly ISender _sender;
    public PartesCategoriasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPartesCategoriasQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await _sender.Send(new GetParteCategoriaByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PartesCategorias item)
    {
        var result = await _sender.Send(new CreateParteCategoriaCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.ParteCategoriaID, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(short id, [FromBody] PartesCategorias item)
    {
        var result = await _sender.Send(new UpdateParteCategoriaCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _sender.Send(new DeleteParteCategoriaCommand(id));
        return result ? NoContent() : NotFound();
    }
}
