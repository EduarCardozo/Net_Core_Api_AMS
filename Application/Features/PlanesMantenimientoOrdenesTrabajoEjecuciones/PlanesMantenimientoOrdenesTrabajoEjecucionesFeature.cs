using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PlanesMantenimientoOrdenesTrabajoEjecuciones;

public record GetAllPlanesMantenimientoOrdenesTrabajoEjecucionesQuery : IRequest<IEnumerable<Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones>>;
public record GetPlanMantenimientoOrdenTrabajoEjecucionByIdQuery(int Id) : IRequest<Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones?>;
public record CreatePlanMantenimientoOrdenTrabajoEjecucionCommand(Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones Item) : IRequest<Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones>;
public record UpdatePlanMantenimientoOrdenTrabajoEjecucionCommand(int Id, Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones Item) : IRequest<bool>;
public record DeletePlanMantenimientoOrdenTrabajoEjecucionCommand(int Id) : IRequest<bool>;

public class PlanesMantenimientoOrdenesTrabajoEjecucionesHandlers :
    IRequestHandler<GetAllPlanesMantenimientoOrdenesTrabajoEjecucionesQuery, IEnumerable<Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones>>,
    IRequestHandler<GetPlanMantenimientoOrdenTrabajoEjecucionByIdQuery, Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones?>,
    IRequestHandler<CreatePlanMantenimientoOrdenTrabajoEjecucionCommand, Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones>,
    IRequestHandler<UpdatePlanMantenimientoOrdenTrabajoEjecucionCommand, bool>,
    IRequestHandler<DeletePlanMantenimientoOrdenTrabajoEjecucionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PlanesMantenimientoOrdenesTrabajoEjecucionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones>> Handle(GetAllPlanesMantenimientoOrdenesTrabajoEjecucionesQuery r, CancellationToken ct)
        => await _context.PlanesMantenimientoOrdenesTrabajoEjecuciones.ToListAsync(ct);

    public async Task<Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones?> Handle(GetPlanMantenimientoOrdenTrabajoEjecucionByIdQuery r, CancellationToken ct)
        => await _context.PlanesMantenimientoOrdenesTrabajoEjecuciones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PlanesMantenimientoOrdenesTrabajoEjecuciones> Handle(CreatePlanMantenimientoOrdenTrabajoEjecucionCommand r, CancellationToken ct)
    {
        _context.PlanesMantenimientoOrdenesTrabajoEjecuciones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdatePlanMantenimientoOrdenTrabajoEjecucionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PlanMantenimientoOrdenTrabajoEjecucionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeletePlanMantenimientoOrdenTrabajoEjecucionCommand r, CancellationToken ct)
    {
        var item = await _context.PlanesMantenimientoOrdenesTrabajoEjecuciones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PlanesMantenimientoOrdenesTrabajoEjecuciones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
