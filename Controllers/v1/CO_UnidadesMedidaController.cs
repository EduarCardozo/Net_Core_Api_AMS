using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.CO_UnidadesMedida;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/co_unidadesmedida")]
public class CO_UnidadesMedidaController : ControllerBase
{
    private readonly ISender _sender;
    public CO_UnidadesMedidaController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllCO_UnidadesMedidaQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(byte id)
    {
        var result = await _sender.Send(new GetCO_UnidadMedidaByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CO_UnidadesMedida item)
    {
        var result = await _sender.Send(new CreateCO_UnidadMedidaCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.UnidadMedidaID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(byte id, [FromBody] CO_UnidadesMedida item)
    {
        var result = await _sender.Send(new UpdateCO_UnidadMedidaCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(byte id)
    {
        var result = await _sender.Send(new DeleteCO_UnidadMedidaCommand(id));
        return result ? NoContent() : NotFound();
    }
}
