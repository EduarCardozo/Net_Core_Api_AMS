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
        var codigoDuplicado = await _context.EquiposTipos
            .AnyAsync(t => t.Codigo == r.Item.Codigo, ct);
        if (codigoDuplicado)
            throw new InvalidOperationException($"Ya existe un tipo de equipo con el código '{r.Item.Codigo}'.");

        var nombreDuplicado = await _context.EquiposTipos
            .AnyAsync(t => t.Nombre == r.Item.Nombre, ct);
        if (nombreDuplicado)
            throw new InvalidOperationException($"Ya existe un tipo de equipo con el nombre '{r.Item.Nombre}'.");

        _context.EquiposTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateEquipoTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.EquipoTipoID) return false;

        var existe = await _context.EquiposTipos.AnyAsync(t => t.EquipoTipoID == r.Id, ct);
        if (!existe) return false;

        var codigoDuplicado = await _context.EquiposTipos
            .AnyAsync(t => t.Codigo == r.Item.Codigo && t.EquipoTipoID != r.Id, ct);
        if (codigoDuplicado)
            throw new InvalidOperationException($"Ya existe otro tipo de equipo con el código '{r.Item.Codigo}'.");

        var nombreDuplicado = await _context.EquiposTipos
            .AnyAsync(t => t.Nombre == r.Item.Nombre && t.EquipoTipoID != r.Id, ct);
        if (nombreDuplicado)
            throw new InvalidOperationException($"Ya existe otro tipo de equipo con el nombre '{r.Item.Nombre}'.");

        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteEquipoTipoCommand r, CancellationToken ct)
    {
        var item = await _context.EquiposTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;

        var tieneEquipos = await _context.Equipos
            .AnyAsync(e => e.EquipoTipoID == r.Id, ct);
        if (tieneEquipos)
            throw new InvalidOperationException("No se puede eliminar el tipo de equipo porque tiene equipos asignados.");

        _context.EquiposTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
