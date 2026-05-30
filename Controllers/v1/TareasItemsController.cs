using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TareasItems;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tareasitems")]
public class TareasItemsController : ControllerBase
{
    private readonly ISender _sender;
    public TareasItemsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTareasItemsQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TareasItems item)
    {
        var result = await _sender.Send(new CreateTareaItemCommand(item));
        return Ok(result);
    }
}