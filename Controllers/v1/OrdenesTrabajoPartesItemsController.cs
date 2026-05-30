using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.OrdenesTrabajoPartesItems;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/ordenestrabajopartesitems")]
public class OrdenesTrabajoPartesItemsController : ControllerBase
{
    private readonly ISender _sender;
    public OrdenesTrabajoPartesItemsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllOrdenesTrabajoPartesItemsQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrdenesTrabajoPartesItems item)
    {
        var result = await _sender.Send(new CreateOrdenTrabajoParteItemCommand(item));
        return Ok(result);
    }
}
