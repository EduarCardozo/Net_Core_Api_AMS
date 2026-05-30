using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PartesItemsOrdenesReparacion;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/partesitemsordenesreparacion")]
public class PartesItemsOrdenesReparacionController : ControllerBase
{
    private readonly ISender _sender;
    public PartesItemsOrdenesReparacionController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPartesItemsOrdenesReparacionQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetParteItemOrdenReparacionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PartesItemsOrdenesReparacion item)
    {
        var result = await _sender.Send(new CreateParteItemOrdenReparacionCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.ParteItemOrdenReparacionID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PartesItemsOrdenesReparacion item)
    {
        var result = await _sender.Send(new UpdateParteItemOrdenReparacionCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteParteItemOrdenReparacionCommand(id));
        return result ? NoContent() : NotFound();
    }
}