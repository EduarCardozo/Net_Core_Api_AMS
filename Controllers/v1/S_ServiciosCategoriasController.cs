using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.S_ServiciosCategorias;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/s_servicioscategorias")]
public class S_ServiciosCategoriasController : ControllerBase
{
    private readonly ISender _sender;
    public S_ServiciosCategoriasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllS_ServiciosCategoriasQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetS_ServicioCategoriaByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] S_ServiciosCategorias item)
    {
        var result = await _sender.Send(new CreateS_ServicioCategoriaCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.S_ServicioCategoriaID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] S_ServiciosCategorias item)
    {
        var result = await _sender.Send(new UpdateS_ServicioCategoriaCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteS_ServicioCategoriaCommand(id));
        return result ? NoContent() : NotFound();
    }
}
