using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.CONST_SistemasExternosEntidadesCodigos_TargetType;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/const_sistemasexternosentidadescodigos_targettype")]
public class CONST_SistemasExternosEntidadesCodigos_TargetTypeController : ControllerBase
{
    private readonly ISender _sender;
    public CONST_SistemasExternosEntidadesCodigos_TargetTypeController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllCONST_SistemasExternosEntidadesCodigos_TargetTypeQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(byte id)
    {
        var result = await _sender.Send(new GetCONST_TargetTypeByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CONST_SistemasExternosEntidadesCodigos_TargetType item)
    {
        var result = await _sender.Send(new CreateCONST_TargetTypeCommand(item));
        return CreatedAtAction(nameof(GetById), new { id = result.TargetTypeId, version = "1.0" }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(byte id, [FromBody] CONST_SistemasExternosEntidadesCodigos_TargetType item)
    {
        var result = await _sender.Send(new UpdateCONST_TargetTypeCommand(id, item));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(byte id)
    {
        var result = await _sender.Send(new DeleteCONST_TargetTypeCommand(id));
        return result ? NoContent() : NotFound();
    }
}
