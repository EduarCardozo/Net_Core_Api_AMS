using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TareasPartes;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tareaspartes")]
public class TareasPartesController : ControllerBase
{
    private readonly ISender _sender;
    public TareasPartesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTareasPartesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TareasPartes item)
    {
        var result = await _sender.Send(new CreateTareaParteCommand(item));
        return Ok(result);
    }
}
