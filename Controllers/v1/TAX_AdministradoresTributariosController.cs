using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_AdministradoresTributarios;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_administradorestributarios")]
public class TAX_AdministradoresTributariosController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_AdministradoresTributariosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_AdministradoresTributariosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetTAX_AdministradorTributarioByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_AdministradoresTributarios item)
    {
        var result = await _sender.Send(new CreateTAX_AdministradorTributarioCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.AdministradorTributarioID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TAX_AdministradoresTributarios item)
    {
        var result = await _sender.Send(new UpdateTAX_AdministradorTributarioCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteTAX_AdministradorTributarioCommand(id));
        return result ? NoContent() : NotFound();
    }
}
