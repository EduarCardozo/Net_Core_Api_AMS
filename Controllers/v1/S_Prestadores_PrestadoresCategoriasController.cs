using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.S_Prestadores_PrestadoresCategorias;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/s_prestadores_prestadorescategorias")]
public class S_Prestadores_PrestadoresCategoriasController : ControllerBase
{
    private readonly ISender _sender;
    public S_Prestadores_PrestadoresCategoriasController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllS_Prestadores_PrestadoresCategoriasQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] S_Prestadores_PrestadoresCategorias item)
    {
        var result = await _sender.Send(new CreateS_Prestador_PrestadorCategoriaCommand(item));
        return Ok(result);
    }
}
