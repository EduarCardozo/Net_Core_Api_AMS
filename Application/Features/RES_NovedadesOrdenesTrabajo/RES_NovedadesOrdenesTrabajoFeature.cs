using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_NovedadesOrdenesTrabajo;

public record GetAllRES_NovedadesOrdenesTrabajoQuery : IRequest<IEnumerable<Ent.RES_NovedadesOrdenesTrabajo>>;
public record GetRES_NovedadOrdenTrabajoByIdQuery(int Id) : IRequest<Ent.RES_NovedadesOrdenesTrabajo?>;
public record CreateRES_NovedadOrdenTrabajoCommand(Ent.RES_NovedadesOrdenesTrabajo Item) : IRequest<Ent.RES_NovedadesOrdenesTrabajo>;
public record UpdateRES_NovedadOrdenTrabajoCommand(int Id, Ent.RES_NovedadesOrdenesTrabajo Item) : IRequest<bool>;
public record DeleteRES_NovedadOrdenTrabajoCommand(int Id) : IRequest<bool>;

public class RES_NovedadesOrdenesTrabajoHandlers :
    IRequestHandler<GetAllRES_NovedadesOrdenesTrabajoQuery, IEnumerable<Ent.RES_NovedadesOrdenesTrabajo>>,
    IRequestHandler<GetRES_NovedadOrdenTrabajoByIdQuery, Ent.RES_NovedadesOrdenesTrabajo?>,
    IRequestHandler<CreateRES_NovedadOrdenTrabajoCommand, Ent.RES_NovedadesOrdenesTrabajo>,
    IRequestHandler<UpdateRES_NovedadOrdenTrabajoCommand, bool>,
    IRequestHandler<DeleteRES_NovedadOrdenTrabajoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_NovedadesOrdenesTrabajoHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_NovedadesOrdenesTrabajo>> Handle(GetAllRES_NovedadesOrdenesTrabajoQuery r, CancellationToken ct)
        => await _context.RES_NovedadesOrdenesTrabajo.ToListAsync(ct);

    public async Task<Ent.RES_NovedadesOrdenesTrabajo?> Handle(GetRES_NovedadOrdenTrabajoByIdQuery r, CancellationToken ct)
        => await _context.RES_NovedadesOrdenesTrabajo.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_NovedadesOrdenesTrabajo> Handle(CreateRES_NovedadOrdenTrabajoCommand r, CancellationToken ct)
    {
        _context.RES_NovedadesOrdenesTrabajo.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_NovedadOrdenTrabajoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.NovedadOrdenTrabajoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_NovedadOrdenTrabajoCommand r, CancellationToken ct)
    {
        var item = await _context.RES_NovedadesOrdenesTrabajo.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_NovedadesOrdenesTrabajo.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
