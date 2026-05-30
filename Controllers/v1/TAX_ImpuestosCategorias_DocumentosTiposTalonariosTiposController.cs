using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/tax_impuestoscategorias_documentostipostalonariotipos")]
public class TAX_ImpuestosCategoriasDocumentosTiposTalonariosTiposController : ControllerBase
{
    private readonly ISender _sender;
    public TAX_ImpuestosCategoriasDocumentosTiposTalonariosTiposController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllTAX_ImpuestosCategoriasDocumentosTiposTalonariosTiposQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos item)
    {
        var result = await _sender.Send(new CreateTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand(item));
        return Ok(result);
    }
}
