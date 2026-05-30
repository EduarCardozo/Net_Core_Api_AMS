using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.S_Servicios;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/s_servicios")]
public class S_ServiciosController : ControllerBase
{
    private readonly ISender _sender;
    public S_ServiciosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllS_ServiciosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetS_ServicioByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] S_Servicios item)
    {
        var result = await _sender.Send(new CreateS_ServicioCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.S_ServicioID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] S_Servicios item)
    {
        var result = await _sender.Send(new UpdateS_ServicioCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteS_ServicioCommand(id));
        return result ? NoContent() : NotFound();
    }
}
