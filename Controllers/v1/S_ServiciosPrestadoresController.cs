using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.S_ServiciosPrestadores;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/s_serviciosprestadores")]
public class S_ServiciosPrestadoresController : ControllerBase
{
    private readonly ISender _sender;
    public S_ServiciosPrestadoresController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllS_ServiciosPrestadoresQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] S_ServiciosPrestadores item)
    {
        var result = await _sender.Send(new CreateS_ServicioPrestadorCommand(item));
        return Ok(result);
    }
}
