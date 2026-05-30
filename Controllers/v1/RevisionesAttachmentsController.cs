using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Api.Application.Features.RevisionesAttachments;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/revisionesattachments")]
public class RevisionesAttachmentsController : ControllerBase
{
    private readonly ISender _sender;
    public RevisionesAttachmentsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _sender.Send(new GetAllRevisionesAttachmentsQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RevisionesAttachments item)
    {
        var result = await _sender.Send(new CreateRevisionAttachmentCommand(item));
        return Ok(result);
    }
}
