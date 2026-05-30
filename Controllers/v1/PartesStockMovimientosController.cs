using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PartesStockMovimientos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/partesstockmovimientos")]
public class PartesStockMovimientosController : ControllerBase
{
    private readonly ISender _sender;
    public PartesStockMovimientosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPartesStockMovimientosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetParteStockMovimientoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PartesStockMovimientos item)
    {
        var result = await _sender.Send(new CreateParteStockMovimientoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.ParteStockMovimientoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PartesStockMovimientos item)
    {
        var result = await _sender.Send(new UpdateParteStockMovimientoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteParteStockMovimientoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
