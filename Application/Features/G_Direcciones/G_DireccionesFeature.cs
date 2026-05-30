using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.G_Direcciones;

public record GetAllG_DireccionesQuery : IRequest<IEnumerable<Ent.G_Direcciones>>;
public record GetG_DireccionByIdQuery(int Id) : IRequest<Ent.G_Direcciones?>;
public record CreateG_DireccionCommand(Ent.G_Direcciones Item) : IRequest<Ent.G_Direcciones>;
public record UpdateG_DireccionCommand(int Id, Ent.G_Direcciones Item) : IRequest<bool>;
public record DeleteG_DireccionCommand(int Id) : IRequest<bool>;

public class G_DireccionesHandlers :
    IRequestHandler<GetAllG_DireccionesQuery, IEnumerable<Ent.G_Direcciones>>,
    IRequestHandler<GetG_DireccionByIdQuery, Ent.G_Direcciones?>,
    IRequestHandler<CreateG_DireccionCommand, Ent.G_Direcciones>,
    IRequestHandler<UpdateG_DireccionCommand, bool>,
    IRequestHandler<DeleteG_DireccionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public G_DireccionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.G_Direcciones>> Handle(GetAllG_DireccionesQuery r, CancellationToken ct)
        => await _context.G_Direcciones.ToListAsync(ct);

    public async Task<Ent.G_Direcciones?> Handle(GetG_DireccionByIdQuery r, CancellationToken ct)
        => await _context.G_Direcciones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.G_Direcciones> Handle(CreateG_DireccionCommand r, CancellationToken ct)
    {
        _context.G_Direcciones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateG_DireccionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.DireccionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteG_DireccionCommand r, CancellationToken ct)
    {
        var item = await _context.G_Direcciones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.G_Direcciones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
