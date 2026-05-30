using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.G_SubRegiones;

public record GetAllG_SubRegionesQuery : IRequest<IEnumerable<Ent.G_SubRegiones>>;
public record GetG_SubRegionByIdQuery(int Id) : IRequest<Ent.G_SubRegiones?>;
public record CreateG_SubRegionCommand(Ent.G_SubRegiones Item) : IRequest<Ent.G_SubRegiones>;
public record UpdateG_SubRegionCommand(int Id, Ent.G_SubRegiones Item) : IRequest<bool>;
public record DeleteG_SubRegionCommand(int Id) : IRequest<bool>;

public class G_SubRegionesHandlers :
    IRequestHandler<GetAllG_SubRegionesQuery, IEnumerable<Ent.G_SubRegiones>>,
    IRequestHandler<GetG_SubRegionByIdQuery, Ent.G_SubRegiones?>,
    IRequestHandler<CreateG_SubRegionCommand, Ent.G_SubRegiones>,
    IRequestHandler<UpdateG_SubRegionCommand, bool>,
    IRequestHandler<DeleteG_SubRegionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public G_SubRegionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.G_SubRegiones>> Handle(GetAllG_SubRegionesQuery r, CancellationToken ct)
        => await _context.G_SubRegiones.ToListAsync(ct);

    public async Task<Ent.G_SubRegiones?> Handle(GetG_SubRegionByIdQuery r, CancellationToken ct)
        => await _context.G_SubRegiones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.G_SubRegiones> Handle(CreateG_SubRegionCommand r, CancellationToken ct)
    {
        _context.G_SubRegiones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateG_SubRegionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.SubRegionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteG_SubRegionCommand r, CancellationToken ct)
    {
        var item = await _context.G_SubRegiones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.G_SubRegiones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
