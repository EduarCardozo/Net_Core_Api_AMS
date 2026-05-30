using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.OrdenesTrabajoPartes;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/ordenestrabajopartes")]
public class OrdenesTrabajoPartesController : ControllerBase
{
    private readonly ISender _sender;
    public OrdenesTrabajoPartesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllOrdenesTrabajoPartesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrdenesTrabajoPartes item)
    {
        var result = await _sender.Send(new CreateOrdenTrabajoParteCommand(item));
        return Ok(result);
    }
}
