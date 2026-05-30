using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PlanesMantenimientoOrdenesTrabajo;

public record GetAllPlanesMantenimientoOrdenesTrabajoQuery : IRequest<IEnumerable<Ent.PlanesMantenimientoOrdenesTrabajo>>;
public record GetPlanMantenimientoOrdenTrabajoByIdQuery(int Id) : IRequest<Ent.PlanesMantenimientoOrdenesTrabajo?>;
public record CreatePlanMantenimientoOrdenTrabajoCommand(Ent.PlanesMantenimientoOrdenesTrabajo Item) : IRequest<Ent.PlanesMantenimientoOrdenesTrabajo>;
public record UpdatePlanMantenimientoOrdenTrabajoCommand(int Id, Ent.PlanesMantenimientoOrdenesTrabajo Item) : IRequest<bool>;
public record DeletePlanMantenimientoOrdenTrabajoCommand(int Id) : IRequest<bool>;

public class PlanesMantenimientoOrdenesTrabajoHandlers :
    IRequestHandler<GetAllPlanesMantenimientoOrdenesTrabajoQuery, IEnumerable<Ent.PlanesMantenimientoOrdenesTrabajo>>,
    IRequestHandler<GetPlanMantenimientoOrdenTrabajoByIdQuery, Ent.PlanesMantenimientoOrdenesTrabajo?>,
    IRequestHandler<CreatePlanMantenimientoOrdenTrabajoCommand, Ent.PlanesMantenimientoOrdenesTrabajo>,
    IRequestHandler<UpdatePlanMantenimientoOrdenTrabajoCommand, bool>,
    IRequestHandler<DeletePlanMantenimientoOrdenTrabajoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PlanesMantenimientoOrdenesTrabajoHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PlanesMantenimientoOrdenesTrabajo>> Handle(GetAllPlanesMantenimientoOrdenesTrabajoQuery r, CancellationToken ct)
        => await _context.PlanesMantenimientoOrdenesTrabajo.ToListAsync(ct);

    public async Task<Ent.PlanesMantenimientoOrdenesTrabajo?> Handle(GetPlanMantenimientoOrdenTrabajoByIdQuery r, CancellationToken ct)
        => await _context.PlanesMantenimientoOrdenesTrabajo.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PlanesMantenimientoOrdenesTrabajo> Handle(CreatePlanMantenimientoOrdenTrabajoCommand r, CancellationToken ct)
    {
        _context.PlanesMantenimientoOrdenesTrabajo.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdatePlanMantenimientoOrdenTrabajoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PlanMantenimientoOrdenTrabajoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeletePlanMantenimientoOrdenTrabajoCommand r, CancellationToken ct)
    {
        var item = await _context.PlanesMantenimientoOrdenesTrabajo.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PlanesMantenimientoOrdenesTrabajo.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
