using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.Connectors;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/connectors")]
public class ConnectorsController : ControllerBase
{
    private readonly ISender _sender;
    public ConnectorsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllConnectorsQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetConnectorByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Connectors item)
    {
        var result = await _sender.Send(new CreateConnectorCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.ConnectorID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Connectors item)
    {
        var result = await _sender.Send(new UpdateConnectorCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteConnectorCommand(id));
        return result ? NoContent() : NotFound();
    }
}
