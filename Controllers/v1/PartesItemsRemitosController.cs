using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PartesItemsRemitos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/partesitemsremitos")]
public class PartesItemsRemitosController : ControllerBase
{
    private readonly ISender _sender;
    public PartesItemsRemitosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPartesItemsRemitosQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PartesItemsRemitos item)
    {
        var result = await _sender.Send(new CreateParteItemRemitoCommand(item));
        return Ok(result);
    }
}
