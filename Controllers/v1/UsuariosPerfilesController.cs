using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.UsuariosPerfiles;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/usuariosperfiles")]
public class UsuariosPerfilesController : ControllerBase
{
    private readonly ISender _sender;
    public UsuariosPerfilesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllUsuariosPerfilesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuariosPerfiles item)
    {
        var result = await _sender.Send(new CreateUsuarioPerfilCommand(item));
        return Ok(result);
    }
}
