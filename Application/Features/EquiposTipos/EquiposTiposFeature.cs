using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.EquiposTipos;

public record GetAllEquiposTiposQuery : IRequest<IEnumerable<Ent.EquiposTipos>>;
public record GetEquipoTipoByIdQuery(byte Id) : IRequest<Ent.EquiposTipos?>;
public record CreateEquipoTipoCommand(Ent.EquiposTipos Item) : IRequest<Ent.EquiposTipos>;
public record UpdateEquipoTipoCommand(byte Id, Ent.EquiposTipos Item) : IRequest<bool>;
public record DeleteEquipoTipoCommand(byte Id) : IRequest<bool>;

public class EquiposTiposHandlers :
    IRequestHandler<GetAllEquiposTiposQuery, IEnumerable<Ent.EquiposTipos>>,
    IRequestHandler<GetEquipoTipoByIdQuery, Ent.EquiposTipos?>,
    IRequestHandler<CreateEquipoTipoCommand, Ent.EquiposTipos>,
    IRequestHandler<UpdateEquipoTipoCommand, bool>,
    IRequestHandler<DeleteEquipoTipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public EquiposTiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.EquiposTipos>> Handle(GetAllEquiposTiposQuery r, CancellationToken ct)
        => await _context.EquiposTipos.ToListAsync(ct);

    public async Task<Ent.EquiposTipos?> Handle(GetEquipoTipoByIdQuery r, CancellationToken ct)
        => await _context.EquiposTipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.EquiposTipos> Handle(CreateEquipoTipoCommand r, CancellationToken ct)
    {
        _context.EquiposTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateEquipoTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.EquipoTipoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteEquipoTipoCommand r, CancellationToken ct)
    {
        var item = await _context.EquiposTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.EquiposTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
