using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Equipos;

public record GetAllEquiposQuery : IRequest<IEnumerable<Ent.Equipos>>;
public record GetEquipoByIdQuery(int Id) : IRequest<Ent.Equipos?>;
public record CreateEquipoCommand(Ent.Equipos Item) : IRequest<Ent.Equipos>;
public record UpdateEquipoCommand(int Id, Ent.Equipos Item) : IRequest<bool>;
public record DeleteEquipoCommand(int Id) : IRequest<bool>;

public class EquiposHandlers :
    IRequestHandler<GetAllEquiposQuery, IEnumerable<Ent.Equipos>>,
    IRequestHandler<GetEquipoByIdQuery, Ent.Equipos?>,
    IRequestHandler<CreateEquipoCommand, Ent.Equipos>,
    IRequestHandler<UpdateEquipoCommand, bool>,
    IRequestHandler<DeleteEquipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public EquiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Equipos>> Handle(GetAllEquiposQuery r, CancellationToken ct)
        => await _context.Equipos.ToListAsync(ct);

    public async Task<Ent.Equipos?> Handle(GetEquipoByIdQuery r, CancellationToken ct)
        => await _context.Equipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Equipos> Handle(CreateEquipoCommand r, CancellationToken ct)
    {
        await ValidarForeignKeys(r.Item, equipoIdExcluir: null, ct);

        if (!string.IsNullOrEmpty(r.Item.Codigo))
        {
            var codigoDuplicado = await _context.Equipos
                .AnyAsync(e => e.Codigo == r.Item.Codigo, ct);
            if (codigoDuplicado)
                throw new InvalidOperationException($"Ya existe un equipo con el código '{r.Item.Codigo}'.");
        }

        _context.Equipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateEquipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.EquipoID) return false;

        var existe = await _context.Equipos.AnyAsync(e => e.EquipoID == r.Id, ct);
        if (!existe) return false;

        await ValidarForeignKeys(r.Item, equipoIdExcluir: r.Id, ct);

        if (!string.IsNullOrEmpty(r.Item.Codigo))
        {
            var codigoDuplicado = await _context.Equipos
                .AnyAsync(e => e.Codigo == r.Item.Codigo && e.EquipoID != r.Id, ct);
            if (codigoDuplicado)
                throw new InvalidOperationException($"Ya existe otro equipo con el código '{r.Item.Codigo}'.");
        }

        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteEquipoCommand r, CancellationToken ct)
    {
        var item = await _context.Equipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;

        var tieneOrdenes = await _context.OrdenesTrabajo
            .AnyAsync(o => o.EquipoID == r.Id, ct);
        if (tieneOrdenes)
            throw new InvalidOperationException("No se puede eliminar el equipo porque tiene órdenes de trabajo asociadas.");

        var tieneRevisiones = await _context.Revisiones
            .AnyAsync(rev => rev.EquipoID == r.Id, ct);
        if (tieneRevisiones)
            throw new InvalidOperationException("No se puede eliminar el equipo porque tiene revisiones asociadas.");

        var tieneNovedades = await _context.NovedadesTecnicas
            .AnyAsync(n => n.EquipoID == r.Id, ct);
        if (tieneNovedades)
            throw new InvalidOperationException("No se puede eliminar el equipo porque tiene novedades técnicas asociadas.");

        var tieneMediciones = await _context.EquiposMediciones
            .AnyAsync(m => m.EquipoID == r.Id, ct);
        if (tieneMediciones)
            throw new InvalidOperationException("No se puede eliminar el equipo porque tiene mediciones registradas.");

        var tienePartes = await _context.EquiposPartesItems
            .AnyAsync(p => p.EquipoID == r.Id, ct);
        if (tienePartes)
            throw new InvalidOperationException("No se puede eliminar el equipo porque tiene partes/items asociados.");

        _context.Equipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    // Valida que las FK apunten a registros existentes y que MarcaModelo pertenezca a la Marca indicada.
    // equipoIdExcluir es null en Create, y el ID del equipo en Update (no se usa aquí pero queda disponible para extensiones).
    private async Task ValidarForeignKeys(Ent.Equipos item, int? equipoIdExcluir, CancellationToken ct)
    {
        var tipoExiste = await _context.EquiposTipos
            .AnyAsync(t => t.EquipoTipoID == item.EquipoTipoID, ct);
        if (!tipoExiste)
            throw new ArgumentException($"El tipo de equipo con ID {item.EquipoTipoID} no existe.");

        if (item.MarcaID.HasValue)
        {
            var marcaExiste = await _context.Marcas
                .AnyAsync(m => m.MarcaID == item.MarcaID.Value, ct);
            if (!marcaExiste)
                throw new ArgumentException($"La marca con ID {item.MarcaID} no existe.");
        }

        if (item.MarcaModeloID.HasValue)
        {
            var modeloExiste = await _context.MarcasModelos
                .AnyAsync(m => m.MarcaModeloID == item.MarcaModeloID.Value
                            && m.MarcaID == item.MarcaID, ct);
            if (!modeloExiste)
                throw new ArgumentException($"El modelo con ID {item.MarcaModeloID} no existe o no pertenece a la marca indicada.");
        }

        if (item.EquipoGrupoID.HasValue)
        {
            var grupoExiste = await _context.EquiposGrupos
                .AnyAsync(g => g.EquipoGrupoID == item.EquipoGrupoID.Value, ct);
            if (!grupoExiste)
                throw new ArgumentException($"El grupo de equipo con ID {item.EquipoGrupoID} no existe.");
        }
    }
}
