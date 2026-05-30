using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_RegimenesTributariosS_Prestadores;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_regimentributarioss_prestadores")]
public class TAX_RegimenesTributariosS_PrestadoresController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_RegimenesTributariosS_PrestadoresController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_RegimenesTributariosS_PrestadoresQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_RegimenesTributariosS_Prestadores item)
    {
        var result = await _sender.Send(new CreateTAX_RegimenTributarioS_PrestadorCommand(item));
        return Ok(result);
    }
}
