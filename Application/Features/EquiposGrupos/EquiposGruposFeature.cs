using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.EquiposGrupos;

public record GetAllEquiposGruposQuery : IRequest<IEnumerable<Ent.EquiposGrupos>>;
public record GetEquipoGrupoByIdQuery(short Id) : IRequest<Ent.EquiposGrupos?>;
public record CreateEquipoGrupoCommand(Ent.EquiposGrupos Item) : IRequest<Ent.EquiposGrupos>;
public record UpdateEquipoGrupoCommand(short Id, Ent.EquiposGrupos Item) : IRequest<bool>;
public record DeleteEquipoGrupoCommand(short Id) : IRequest<bool>;

public class EquiposGruposHandlers :
    IRequestHandler<GetAllEquiposGruposQuery, IEnumerable<Ent.EquiposGrupos>>,
    IRequestHandler<GetEquipoGrupoByIdQuery, Ent.EquiposGrupos?>,
    IRequestHandler<CreateEquipoGrupoCommand, Ent.EquiposGrupos>,
    IRequestHandler<UpdateEquipoGrupoCommand, bool>,
    IRequestHandler<DeleteEquipoGrupoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public EquiposGruposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.EquiposGrupos>> Handle(GetAllEquiposGruposQuery r, CancellationToken ct)
        => await _context.EquiposGrupos.ToListAsync(ct);

    public async Task<Ent.EquiposGrupos?> Handle(GetEquipoGrupoByIdQuery r, CancellationToken ct)
        => await _context.EquiposGrupos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.EquiposGrupos> Handle(CreateEquipoGrupoCommand r, CancellationToken ct)
    {
        var nombreDuplicado = await _context.EquiposGrupos
            .AnyAsync(g => g.Nombre == r.Item.Nombre, ct);
        if (nombreDuplicado)
            throw new InvalidOperationException($"Ya existe un grupo con el nombre '{r.Item.Nombre}'.");

        _context.EquiposGrupos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateEquipoGrupoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.EquipoGrupoID) return false;

        var existe = await _context.EquiposGrupos.AnyAsync(g => g.EquipoGrupoID == r.Id, ct);
        if (!existe) return false;

        var nombreDuplicado = await _context.EquiposGrupos
            .AnyAsync(g => g.Nombre == r.Item.Nombre && g.EquipoGrupoID != r.Id, ct);
        if (nombreDuplicado)
            throw new InvalidOperationException($"Ya existe otro grupo con el nombre '{r.Item.Nombre}'.");

        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteEquipoGrupoCommand r, CancellationToken ct)
    {
        var item = await _context.EquiposGrupos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;

        var tieneEquipos = await _context.Equipos
            .AnyAsync(e => e.EquipoGrupoID == r.Id, ct);
        if (tieneEquipos)
            throw new InvalidOperationException("No se puede eliminar el grupo porque tiene equipos asignados.");

        _context.EquiposGrupos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
