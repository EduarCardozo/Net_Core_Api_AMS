using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.PlantillasItems;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/plantillasitems")]
public class PlantillasItemsController : ControllerBase
{
    private readonly ISender _sender;
    public PlantillasItemsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllPlantillasItemsQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlantillasItems item)
    {
        var result = await _sender.Send(new CreatePlantillaItemCommand(item));
        return Ok(result);
    }
}
