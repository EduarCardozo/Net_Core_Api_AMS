using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_NovedadesPlanesMantenimiento;

public record GetAllRES_NovedadesPlanesMantenimientoQuery : IRequest<IEnumerable<Ent.RES_NovedadesPlanesMantenimiento>>;
public record GetRES_NovedadPlanMantenimientoByIdQuery(int Id) : IRequest<Ent.RES_NovedadesPlanesMantenimiento?>;
public record CreateRES_NovedadPlanMantenimientoCommand(Ent.RES_NovedadesPlanesMantenimiento Item) : IRequest<Ent.RES_NovedadesPlanesMantenimiento>;
public record UpdateRES_NovedadPlanMantenimientoCommand(int Id, Ent.RES_NovedadesPlanesMantenimiento Item) : IRequest<bool>;
public record DeleteRES_NovedadPlanMantenimientoCommand(int Id) : IRequest<bool>;

public class RES_NovedadesPlanesMantenimientoHandlers :
    IRequestHandler<GetAllRES_NovedadesPlanesMantenimientoQuery, IEnumerable<Ent.RES_NovedadesPlanesMantenimiento>>,
    IRequestHandler<GetRES_NovedadPlanMantenimientoByIdQuery, Ent.RES_NovedadesPlanesMantenimiento?>,
    IRequestHandler<CreateRES_NovedadPlanMantenimientoCommand, Ent.RES_NovedadesPlanesMantenimiento>,
    IRequestHandler<UpdateRES_NovedadPlanMantenimientoCommand, bool>,
    IRequestHandler<DeleteRES_NovedadPlanMantenimientoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_NovedadesPlanesMantenimientoHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_NovedadesPlanesMantenimiento>> Handle(GetAllRES_NovedadesPlanesMantenimientoQuery r, CancellationToken ct)
        => await _context.RES_NovedadesPlanesMantenimiento.ToListAsync(ct);

    public async Task<Ent.RES_NovedadesPlanesMantenimiento?> Handle(GetRES_NovedadPlanMantenimientoByIdQuery r, CancellationToken ct)
        => await _context.RES_NovedadesPlanesMantenimiento.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_NovedadesPlanesMantenimiento> Handle(CreateRES_NovedadPlanMantenimientoCommand r, CancellationToken ct)
    {
        _context.RES_NovedadesPlanesMantenimiento.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_NovedadPlanMantenimientoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.NovedadPlanMantenimientoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_NovedadPlanMantenimientoCommand r, CancellationToken ct)
    {
        var item = await _context.RES_NovedadesPlanesMantenimiento.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_NovedadesPlanesMantenimiento.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
