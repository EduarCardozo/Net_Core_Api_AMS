using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.OrdenesTrabajoOperaciones;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/ordenestrabajooperaciones")]
public class OrdenesTrabajoOperacionesController : ControllerBase
{
    private readonly ISender _sender;
    public OrdenesTrabajoOperacionesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllOrdenesTrabajoOperacionesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetOrdenTrabajoOperacionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrdenesTrabajoOperaciones item)
    {
        var result = await _sender.Send(new CreateOrdenTrabajoOperacionCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.OrdenTrabajoOperacionID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrdenesTrabajoOperaciones item)
    {
        var result = await _sender.Send(new UpdateOrdenTrabajoOperacionCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteOrdenTrabajoOperacionCommand(id));
        return result ? NoContent() : NotFound();
    }
}
