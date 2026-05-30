using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PlanesMantenimiento;

public record GetAllPlanesMantenimientoQuery : IRequest<IEnumerable<Ent.PlanesMantenimiento>>;
public record GetPlanMantenimientoByIdQuery(int Id) : IRequest<Ent.PlanesMantenimiento?>;
public record CreatePlanMantenimientoCommand(Ent.PlanesMantenimiento Item) : IRequest<Ent.PlanesMantenimiento>;
public record UpdatePlanMantenimientoCommand(int Id, Ent.PlanesMantenimiento Item) : IRequest<bool>;
public record DeletePlanMantenimientoCommand(int Id) : IRequest<bool>;

public class PlanesMantenimientoHandlers :
    IRequestHandler<GetAllPlanesMantenimientoQuery, IEnumerable<Ent.PlanesMantenimiento>>,
    IRequestHandler<GetPlanMantenimientoByIdQuery, Ent.PlanesMantenimiento?>,
    IRequestHandler<CreatePlanMantenimientoCommand, Ent.PlanesMantenimiento>,
    IRequestHandler<UpdatePlanMantenimientoCommand, bool>,
    IRequestHandler<DeletePlanMantenimientoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PlanesMantenimientoHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PlanesMantenimiento>> Handle(GetAllPlanesMantenimientoQuery r, CancellationToken ct)
        => await _context.PlanesMantenimiento.ToListAsync(ct);

    public async Task<Ent.PlanesMantenimiento?> Handle(GetPlanMantenimientoByIdQuery r, CancellationToken ct)
        => await _context.PlanesMantenimiento.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PlanesMantenimiento> Handle(CreatePlanMantenimientoCommand r, CancellationToken ct)
    {
        _context.PlanesMantenimiento.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdatePlanMantenimientoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PlanMantenimientoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeletePlanMantenimientoCommand r, CancellationToken ct)
    {
        var item = await _context.PlanesMantenimiento.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PlanesMantenimiento.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
