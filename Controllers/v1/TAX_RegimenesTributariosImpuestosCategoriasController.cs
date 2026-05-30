using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_RegimenesTributariosImpuestosCategorias;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_regimentributariosimpuestoscategorias")]
public class TAX_RegimenesTributariosImpuestosCategoriasController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_RegimenesTributariosImpuestosCategoriasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_RegimenesTributariosImpuestosCategoriasQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_RegimenesTributariosImpuestosCategorias item)
    {
        var result = await _sender.Send(new CreateTAX_RegimenTributarioImpuestoCategoriaCommand(item));
        return Ok(result);
    }
}
