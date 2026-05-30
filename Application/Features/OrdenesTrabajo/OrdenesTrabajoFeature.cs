using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.OrdenesTrabajo;

public record GetAllOrdenesTrabajoQuery : IRequest<IEnumerable<Ent.OrdenesTrabajo>>;
public record GetOrdenTrabajoByIdQuery(int Id) : IRequest<Ent.OrdenesTrabajo?>;
public record CreateOrdenTrabajoCommand(Ent.OrdenesTrabajo Item) : IRequest<Ent.OrdenesTrabajo>;
public record UpdateOrdenTrabajoCommand(int Id, Ent.OrdenesTrabajo Item) : IRequest<bool>;
public record DeleteOrdenTrabajoCommand(int Id) : IRequest<bool>;

public class OrdenesTrabajoHandlers :
    IRequestHandler<GetAllOrdenesTrabajoQuery, IEnumerable<Ent.OrdenesTrabajo>>,
    IRequestHandler<GetOrdenTrabajoByIdQuery, Ent.OrdenesTrabajo?>,
    IRequestHandler<CreateOrdenTrabajoCommand, Ent.OrdenesTrabajo>,
    IRequestHandler<UpdateOrdenTrabajoCommand, bool>,
    IRequestHandler<DeleteOrdenTrabajoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public OrdenesTrabajoHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.OrdenesTrabajo>> Handle(GetAllOrdenesTrabajoQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajo.ToListAsync(ct);

    public async Task<Ent.OrdenesTrabajo?> Handle(GetOrdenTrabajoByIdQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajo.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.OrdenesTrabajo> Handle(CreateOrdenTrabajoCommand r, CancellationToken ct)
    {
        _context.OrdenesTrabajo.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateOrdenTrabajoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.OrdenTrabajoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteOrdenTrabajoCommand r, CancellationToken ct)
    {
        var item = await _context.OrdenesTrabajo.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.OrdenesTrabajo.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
