using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RemitosPartes;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/remitospartes")]
public class RemitosPartesController : ControllerBase
{
    private readonly ISender _sender;
    public RemitosPartesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRemitosPartesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RemitosPartes item)
    {
        var result = await _sender.Send(new CreateRemitoParteCommand(item));
        return Ok(result);
    }
}
