using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.OrdenesTrabajoOperaciones;

public record GetAllOrdenesTrabajoOperacionesQuery : IRequest<IEnumerable<Ent.OrdenesTrabajoOperaciones>>;
public record GetOrdenTrabajoOperacionByIdQuery(int Id) : IRequest<Ent.OrdenesTrabajoOperaciones?>;
public record CreateOrdenTrabajoOperacionCommand(Ent.OrdenesTrabajoOperaciones Item) : IRequest<Ent.OrdenesTrabajoOperaciones>;
public record UpdateOrdenTrabajoOperacionCommand(int Id, Ent.OrdenesTrabajoOperaciones Item) : IRequest<bool>;
public record DeleteOrdenTrabajoOperacionCommand(int Id) : IRequest<bool>;

public class OrdenesTrabajoOperacionesHandlers :
    IRequestHandler<GetAllOrdenesTrabajoOperacionesQuery, IEnumerable<Ent.OrdenesTrabajoOperaciones>>,
    IRequestHandler<GetOrdenTrabajoOperacionByIdQuery, Ent.OrdenesTrabajoOperaciones?>,
    IRequestHandler<CreateOrdenTrabajoOperacionCommand, Ent.OrdenesTrabajoOperaciones>,
    IRequestHandler<UpdateOrdenTrabajoOperacionCommand, bool>,
    IRequestHandler<DeleteOrdenTrabajoOperacionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public OrdenesTrabajoOperacionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.OrdenesTrabajoOperaciones>> Handle(GetAllOrdenesTrabajoOperacionesQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajoOperaciones.ToListAsync(ct);

    public async Task<Ent.OrdenesTrabajoOperaciones?> Handle(GetOrdenTrabajoOperacionByIdQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajoOperaciones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.OrdenesTrabajoOperaciones> Handle(CreateOrdenTrabajoOperacionCommand r, CancellationToken ct)
    {
        _context.OrdenesTrabajoOperaciones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateOrdenTrabajoOperacionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.OrdenTrabajoOperacionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteOrdenTrabajoOperacionCommand r, CancellationToken ct)
    {
        var item = await _context.OrdenesTrabajoOperaciones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.OrdenesTrabajoOperaciones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
