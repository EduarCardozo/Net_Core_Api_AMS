using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.LicenciasLogs;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/licenciaslogs")]
public class LicenciasLogsController : ControllerBase
{
    private readonly ISender _sender;
    public LicenciasLogsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllLicenciasLogsQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetLicenciaLogByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LicenciasLogs item)
    {
        var result = await _sender.Send(new CreateLicenciaLogCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.LicenciaLogID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] LicenciasLogs item)
    {
        var result = await _sender.Send(new UpdateLicenciaLogCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteLicenciaLogCommand(id));
        return result ? NoContent() : NotFound();
    }
}
