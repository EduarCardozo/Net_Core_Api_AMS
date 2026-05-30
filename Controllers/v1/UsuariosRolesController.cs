using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.UsuariosRoles;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/usuariosroles")]
public class UsuariosRolesController : ControllerBase
{
    private readonly ISender _sender;
    public UsuariosRolesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllUsuariosRolesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuariosRoles item)
    {
        var result = await _sender.Send(new CreateUsuarioRolCommand(item));
        return Ok(result);
    }
}
