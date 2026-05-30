using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.S_Prestadores;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/s_prestadores")]
public class S_PrestadoresController : ControllerBase
{
    private readonly ISender _sender;
    public S_PrestadoresController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllS_PrestadoresQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetS_PrestadorByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] S_Prestadores item)
    {
        var result = await _sender.Send(new CreateS_PrestadorCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.PrestadorID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] S_Prestadores item)
    {
        var result = await _sender.Send(new UpdateS_PrestadorCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteS_PrestadorCommand(id));
        return result ? NoContent() : NotFound();
    }
}
