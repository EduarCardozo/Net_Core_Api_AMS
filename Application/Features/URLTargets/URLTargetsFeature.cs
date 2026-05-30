using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.URLTargets;

public record GetAllURLTargetsQuery : IRequest<IEnumerable<Ent.URLTargets>>;
public record GetURLTargetByIdQuery(int Id) : IRequest<Ent.URLTargets?>;
public record CreateURLTargetCommand(Ent.URLTargets Item) : IRequest<Ent.URLTargets>;
public record UpdateURLTargetCommand(int Id, Ent.URLTargets Item) : IRequest<bool>;
public record DeleteURLTargetCommand(int Id) : IRequest<bool>;

public class URLTargetsHandlers :
    IRequestHandler<GetAllURLTargetsQuery, IEnumerable<Ent.URLTargets>>,
    IRequestHandler<GetURLTargetByIdQuery, Ent.URLTargets?>,
    IRequestHandler<CreateURLTargetCommand, Ent.URLTargets>,
    IRequestHandler<UpdateURLTargetCommand, bool>,
    IRequestHandler<DeleteURLTargetCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public URLTargetsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.URLTargets>> Handle(GetAllURLTargetsQuery r, CancellationToken ct)
        => await _context.URLTargets.ToListAsync(ct);

    public async Task<Ent.URLTargets?> Handle(GetURLTargetByIdQuery r, CancellationToken ct)
        => await _context.URLTargets.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.URLTargets> Handle(CreateURLTargetCommand r, CancellationToken ct)
    {
        _context.URLTargets.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateURLTargetCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.URLTargetID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteURLTargetCommand r, CancellationToken ct)
    {
        var item = await _context.URLTargets.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.URLTargets.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
