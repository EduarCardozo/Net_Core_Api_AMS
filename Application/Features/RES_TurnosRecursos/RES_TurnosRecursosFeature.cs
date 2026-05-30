using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_TurnosRecursos;

public record GetAllRES_TurnosRecursosQuery : IRequest<IEnumerable<Ent.RES_TurnosRecursos>>;
public record GetRES_TurnoRecursoByIdQuery(int Id) : IRequest<Ent.RES_TurnosRecursos?>;
public record CreateRES_TurnoRecursoCommand(Ent.RES_TurnosRecursos Item) : IRequest<Ent.RES_TurnosRecursos>;
public record UpdateRES_TurnoRecursoCommand(int Id, Ent.RES_TurnosRecursos Item) : IRequest<bool>;
public record DeleteRES_TurnoRecursoCommand(int Id) : IRequest<bool>;

public class RES_TurnosRecursosHandlers :
    IRequestHandler<GetAllRES_TurnosRecursosQuery, IEnumerable<Ent.RES_TurnosRecursos>>,
    IRequestHandler<GetRES_TurnoRecursoByIdQuery, Ent.RES_TurnosRecursos?>,
    IRequestHandler<CreateRES_TurnoRecursoCommand, Ent.RES_TurnosRecursos>,
    IRequestHandler<UpdateRES_TurnoRecursoCommand, bool>,
    IRequestHandler<DeleteRES_TurnoRecursoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_TurnosRecursosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_TurnosRecursos>> Handle(GetAllRES_TurnosRecursosQuery r, CancellationToken ct)
        => await _context.RES_TurnosRecursos.ToListAsync(ct);

    public async Task<Ent.RES_TurnosRecursos?> Handle(GetRES_TurnoRecursoByIdQuery r, CancellationToken ct)
        => await _context.RES_TurnosRecursos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_TurnosRecursos> Handle(CreateRES_TurnoRecursoCommand r, CancellationToken ct)
    {
        _context.RES_TurnosRecursos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_TurnoRecursoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.TurnoRecursoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_TurnoRecursoCommand r, CancellationToken ct)
    {
        var item = await _context.RES_TurnosRecursos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_TurnosRecursos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
