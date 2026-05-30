using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.G_DireccionesOwners;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/g_direccionesowners")]
public class G_DireccionesOwnersController : ControllerBase
{
    private readonly ISender _sender;
    public G_DireccionesOwnersController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllG_DireccionesOwnersQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] G_DireccionesOwners item)
    {
        var result = await _sender.Send(new CreateG_DireccionOwnerCommand(item));
        return Ok(result);
    }
}
