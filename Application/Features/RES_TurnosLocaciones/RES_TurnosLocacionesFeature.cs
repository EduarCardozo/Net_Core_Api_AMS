using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_TurnosLocaciones;

public record GetAllRES_TurnosLocacionesQuery : IRequest<IEnumerable<Ent.RES_TurnosLocaciones>>;
public record GetRES_TurnoLocacionByIdQuery(int Id) : IRequest<Ent.RES_TurnosLocaciones?>;
public record CreateRES_TurnoLocacionCommand(Ent.RES_TurnosLocaciones Item) : IRequest<Ent.RES_TurnosLocaciones>;
public record UpdateRES_TurnoLocacionCommand(int Id, Ent.RES_TurnosLocaciones Item) : IRequest<bool>;
public record DeleteRES_TurnoLocacionCommand(int Id) : IRequest<bool>;

public class RES_TurnosLocacionesHandlers :
    IRequestHandler<GetAllRES_TurnosLocacionesQuery, IEnumerable<Ent.RES_TurnosLocaciones>>,
    IRequestHandler<GetRES_TurnoLocacionByIdQuery, Ent.RES_TurnosLocaciones?>,
    IRequestHandler<CreateRES_TurnoLocacionCommand, Ent.RES_TurnosLocaciones>,
    IRequestHandler<UpdateRES_TurnoLocacionCommand, bool>,
    IRequestHandler<DeleteRES_TurnoLocacionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_TurnosLocacionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_TurnosLocaciones>> Handle(GetAllRES_TurnosLocacionesQuery r, CancellationToken ct)
        => await _context.RES_TurnosLocaciones.ToListAsync(ct);

    public async Task<Ent.RES_TurnosLocaciones?> Handle(GetRES_TurnoLocacionByIdQuery r, CancellationToken ct)
        => await _context.RES_TurnosLocaciones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_TurnosLocaciones> Handle(CreateRES_TurnoLocacionCommand r, CancellationToken ct)
    {
        _context.RES_TurnosLocaciones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_TurnoLocacionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.TurnoLocacionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_TurnoLocacionCommand r, CancellationToken ct)
    {
        var item = await _context.RES_TurnosLocaciones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_TurnosLocaciones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
