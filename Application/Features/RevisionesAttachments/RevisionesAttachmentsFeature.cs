using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RevisionesAttachments;

public record GetAllRevisionesAttachmentsQuery : IRequest<IEnumerable<Ent.RevisionesAttachments>>;
public record CreateRevisionAttachmentCommand(Ent.RevisionesAttachments Item) : IRequest<Ent.RevisionesAttachments>;

public class RevisionesAttachmentsHandlers :
    IRequestHandler<GetAllRevisionesAttachmentsQuery, IEnumerable<Ent.RevisionesAttachments>>,
    IRequestHandler<CreateRevisionAttachmentCommand, Ent.RevisionesAttachments>
{
    private readonly AmsFlotDbContext _context;
    public RevisionesAttachmentsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RevisionesAttachments>> Handle(GetAllRevisionesAttachmentsQuery r, CancellationToken ct)
        => await _context.RevisionesAttachments.ToListAsync(ct);

    public async Task<Ent.RevisionesAttachments> Handle(CreateRevisionAttachmentCommand r, CancellationToken ct)
    {
        _context.RevisionesAttachments.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
