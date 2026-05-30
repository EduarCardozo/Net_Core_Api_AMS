using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_AdministradoresTributariosActividadesEconomicas;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_administradorestributariosactividadeseconomicas")]
public class TAX_AdministradoresTributariosActividadesEconomicasController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_AdministradoresTributariosActividadesEconomicasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_AdministradoresTributariosActividadesEconomicasQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_AdministradoresTributariosActividadesEconomicas item)
    {
        var result = await _sender.Send(new CreateTAX_AdministradorTributarioActividadEconomicaCommand(item));
        return Ok(result);
    }
}
