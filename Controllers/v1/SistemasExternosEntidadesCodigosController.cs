using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.SistemasExternosEntidadesCodigos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/sistemasexternosentidadescodigos")]
public class SistemasExternosEntidadesCodigosController : ControllerBase
{
    private readonly ISender _sender;
    public SistemasExternosEntidadesCodigosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllSistemasExternosEntidadesCodigosQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetSistemaExternoEntidadCodigoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SistemasExternosEntidadesCodigos item)
    {
        var result = await _sender.Send(new CreateSistemaExternoEntidadCodigoCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.SistemaExternoEntidadCodigoID, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SistemasExternosEntidadesCodigos item)
    {
        var result = await _sender.Send(new UpdateSistemaExternoEntidadCodigoCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteSistemaExternoEntidadCodigoCommand(id));
        return result ? NoContent() : NotFound();
    }
}
