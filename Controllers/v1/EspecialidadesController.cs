using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.Especialidades;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/especialidades")]
public class EspecialidadesController : ControllerBase
{
    private readonly ISender _sender;
    public EspecialidadesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllEspecialidadesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetEspecialidadByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Especialidades item)
    {
        var result = await _sender.Send(new CreateEspecialidadCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.EspecialidadID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Especialidades item)
    {
        var result = await _sender.Send(new UpdateEspecialidadCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteEspecialidadCommand(id));
        return result ? NoContent() : NotFound();
    }
}
