using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PerfilesRoles;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/perfilesroles")]
public class PerfilesRolesController : ControllerBase
{
    private readonly ISender _sender;
    public PerfilesRolesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPerfilesRolesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PerfilesRoles item)
    {
        var result = await _sender.Send(new CreatePerfilRolCommand(item));
        return Ok(result);
    }
}
