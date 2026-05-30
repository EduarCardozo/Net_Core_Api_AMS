using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.G_Regiones;

public record GetAllG_RegionesQuery : IRequest<IEnumerable<Ent.G_Regiones>>;
public record GetG_RegionByIdQuery(int Id) : IRequest<Ent.G_Regiones?>;
public record CreateG_RegionCommand(Ent.G_Regiones Item) : IRequest<Ent.G_Regiones>;
public record UpdateG_RegionCommand(int Id, Ent.G_Regiones Item) : IRequest<bool>;
public record DeleteG_RegionCommand(int Id) : IRequest<bool>;

public class G_RegionesHandlers :
    IRequestHandler<GetAllG_RegionesQuery, IEnumerable<Ent.G_Regiones>>,
    IRequestHandler<GetG_RegionByIdQuery, Ent.G_Regiones?>,
    IRequestHandler<CreateG_RegionCommand, Ent.G_Regiones>,
    IRequestHandler<UpdateG_RegionCommand, bool>,
    IRequestHandler<DeleteG_RegionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public G_RegionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.G_Regiones>> Handle(GetAllG_RegionesQuery r, CancellationToken ct)
        => await _context.G_Regiones.ToListAsync(ct);

    public async Task<Ent.G_Regiones?> Handle(GetG_RegionByIdQuery r, CancellationToken ct)
        => await _context.G_Regiones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.G_Regiones> Handle(CreateG_RegionCommand r, CancellationToken ct)
    {
        _context.G_Regiones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateG_RegionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.RegionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteG_RegionCommand r, CancellationToken ct)
    {
        var item = await _context.G_Regiones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.G_Regiones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
