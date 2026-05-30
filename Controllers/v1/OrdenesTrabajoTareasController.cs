using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.OrdenesTrabajoTareas;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/ordenestrabajotareas")]
public class OrdenesTrabajoTareasController : ControllerBase
{
    private readonly ISender _sender;
    public OrdenesTrabajoTareasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllOrdenesTrabajoTareasQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrdenesTrabajoTareas item)
    {
        var result = await _sender.Send(new CreateOrdenTrabajoTareaCommand(item));
        return Ok(result);
    }
}
