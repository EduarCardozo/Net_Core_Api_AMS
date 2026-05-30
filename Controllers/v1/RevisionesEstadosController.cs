using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RevisionesEstados;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/revisionesestados")]
public class RevisionesEstadosController : ControllerBase
{
    private readonly ISender _sender;
    public RevisionesEstadosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRevisionesEstadosQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RevisionesEstados item)
    {
        var result = await _sender.Send(new CreateRevisionEstadoCommand(item));
        return Ok(result);
    }
}
