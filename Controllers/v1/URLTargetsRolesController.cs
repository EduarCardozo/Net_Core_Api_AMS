using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.URLTargetsRoles;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/urltargetsroles")]
public class URLTargetsRolesController : ControllerBase
{
    private readonly ISender _sender;
    public URLTargetsRolesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllURLTargetsRolesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] URLTargetsRoles item)
    {
        var result = await _sender.Send(new CreateURLTargetRolCommand(item));
        return Ok(result);
    }
}
