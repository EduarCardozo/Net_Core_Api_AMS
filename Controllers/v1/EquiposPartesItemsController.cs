using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.EquiposPartesItems;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/equipospartesitems")]
public class EquiposPartesItemsController : ControllerBase
{
    private readonly ISender _sender;
    public EquiposPartesItemsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllEquiposPartesItemsQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EquiposPartesItems item)
    {
        var result = await _sender.Send(new CreateEquipoParteItemCommand(item));
        return Ok(result);
    }
}
