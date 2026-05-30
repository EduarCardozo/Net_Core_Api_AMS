using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_Turnos;

public record GetAllRES_TurnosQuery : IRequest<IEnumerable<Ent.RES_Turnos>>;
public record GetRES_TurnoByIdQuery(int Id) : IRequest<Ent.RES_Turnos?>;
public record CreateRES_TurnoCommand(Ent.RES_Turnos Item) : IRequest<Ent.RES_Turnos>;
public record UpdateRES_TurnoCommand(int Id, Ent.RES_Turnos Item) : IRequest<bool>;
public record DeleteRES_TurnoCommand(int Id) : IRequest<bool>;

public class RES_TurnosHandlers :
    IRequestHandler<GetAllRES_TurnosQuery, IEnumerable<Ent.RES_Turnos>>,
    IRequestHandler<GetRES_TurnoByIdQuery, Ent.RES_Turnos?>,
    IRequestHandler<CreateRES_TurnoCommand, Ent.RES_Turnos>,
    IRequestHandler<UpdateRES_TurnoCommand, bool>,
    IRequestHandler<DeleteRES_TurnoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_TurnosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_Turnos>> Handle(GetAllRES_TurnosQuery r, CancellationToken ct)
        => await _context.RES_Turnos.ToListAsync(ct);

    public async Task<Ent.RES_Turnos?> Handle(GetRES_TurnoByIdQuery r, CancellationToken ct)
        => await _context.RES_Turnos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_Turnos> Handle(CreateRES_TurnoCommand r, CancellationToken ct)
    {
        _context.RES_Turnos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_TurnoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.TurnoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_TurnoCommand r, CancellationToken ct)
    {
        var item = await _context.RES_Turnos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_Turnos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
