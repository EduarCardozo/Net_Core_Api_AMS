using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.UsuariosEspecialidades;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/usuariosespecialidades")]
public class UsuariosEspecialidadesController : ControllerBase
{
    private readonly ISender _sender;
    public UsuariosEspecialidadesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllUsuariosEspecialidadesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuariosEspecialidades item)
    {
        var result = await _sender.Send(new CreateUsuarioEspecialidadCommand(item));
        return Ok(result);
    }
}
