using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PerfilesURLTargets;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/perfilesurltargets")]
public class PerfilesURLTargetsController : ControllerBase
{
    private readonly ISender _sender;
    public PerfilesURLTargetsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPerfilesURLTargetsQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PerfilesURLTargets item)
    {
        var result = await _sender.Send(new CreatePerfilURLTargetCommand(item));
        return Ok(result);
    }
}
