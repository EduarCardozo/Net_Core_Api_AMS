using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.LicenciasMoviles;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/licenciasmoviles")]
public class LicenciasMovilesController : ControllerBase
{
    private readonly ISender _sender;
    public LicenciasMovilesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllLicenciasMovilesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetLicenciaMovilByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LicenciasMoviles item)
    {
        var result = await _sender.Send(new CreateLicenciaMovilCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.LicenciaMovilID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] LicenciasMoviles item)
    {
        var result = await _sender.Send(new UpdateLicenciaMovilCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteLicenciaMovilCommand(id));
        return result ? NoContent() : NotFound();
    }
}
