using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.OrdenesTrabajoDetalles;

public record GetAllOrdenesTrabajoDetallesQuery : IRequest<IEnumerable<Ent.OrdenesTrabajoDetalles>>;
public record GetOrdenTrabajoDetalleByIdQuery(int Id) : IRequest<Ent.OrdenesTrabajoDetalles?>;
public record CreateOrdenTrabajoDetalleCommand(Ent.OrdenesTrabajoDetalles Item) : IRequest<Ent.OrdenesTrabajoDetalles>;
public record UpdateOrdenTrabajoDetalleCommand(int Id, Ent.OrdenesTrabajoDetalles Item) : IRequest<bool>;
public record DeleteOrdenTrabajoDetalleCommand(int Id) : IRequest<bool>;

public class OrdenesTrabajoDetallesHandlers :
    IRequestHandler<GetAllOrdenesTrabajoDetallesQuery, IEnumerable<Ent.OrdenesTrabajoDetalles>>,
    IRequestHandler<GetOrdenTrabajoDetalleByIdQuery, Ent.OrdenesTrabajoDetalles?>,
    IRequestHandler<CreateOrdenTrabajoDetalleCommand, Ent.OrdenesTrabajoDetalles>,
    IRequestHandler<UpdateOrdenTrabajoDetalleCommand, bool>,
    IRequestHandler<DeleteOrdenTrabajoDetalleCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public OrdenesTrabajoDetallesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.OrdenesTrabajoDetalles>> Handle(GetAllOrdenesTrabajoDetallesQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajoDetalles.ToListAsync(ct);

    public async Task<Ent.OrdenesTrabajoDetalles?> Handle(GetOrdenTrabajoDetalleByIdQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajoDetalles.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.OrdenesTrabajoDetalles> Handle(CreateOrdenTrabajoDetalleCommand r, CancellationToken ct)
    {
        _context.OrdenesTrabajoDetalles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateOrdenTrabajoDetalleCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.OrdenTrabajoDetalleID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteOrdenTrabajoDetalleCommand r, CancellationToken ct)
    {
        var item = await _context.OrdenesTrabajoDetalles.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.OrdenesTrabajoDetalles.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
