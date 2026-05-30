using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.S_PrestadoresCategorias;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/s_prestadorescategorias")]
public class S_PrestadoresCategoriasController : ControllerBase
{
    private readonly ISender _sender;
    public S_PrestadoresCategoriasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllS_PrestadoresCategoriasQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await _sender.Send(new GetS_PrestadorCategoriaByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] S_PrestadoresCategorias item)
    {
        var result = await _sender.Send(new CreateS_PrestadorCategoriaCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.PrestadorCategoriaID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(short id, [FromBody] S_PrestadoresCategorias item)
    {
        var result = await _sender.Send(new UpdateS_PrestadorCategoriaCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _sender.Send(new DeleteS_PrestadorCategoriaCommand(id));
        return result ? NoContent() : NotFound();
    }
}
