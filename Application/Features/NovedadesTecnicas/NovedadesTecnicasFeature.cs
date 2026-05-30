using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.NovedadesTecnicas;

public record GetAllNovedadesTecnicasQuery : IRequest<IEnumerable<Ent.NovedadesTecnicas>>;
public record GetNovedadTecnicaByIdQuery(int Id) : IRequest<Ent.NovedadesTecnicas?>;
public record CreateNovedadTecnicaCommand(Ent.NovedadesTecnicas Item) : IRequest<Ent.NovedadesTecnicas>;
public record UpdateNovedadTecnicaCommand(int Id, Ent.NovedadesTecnicas Item) : IRequest<bool>;
public record DeleteNovedadTecnicaCommand(int Id) : IRequest<bool>;

public class NovedadesTecnicasHandlers :
    IRequestHandler<GetAllNovedadesTecnicasQuery, IEnumerable<Ent.NovedadesTecnicas>>,
    IRequestHandler<GetNovedadTecnicaByIdQuery, Ent.NovedadesTecnicas?>,
    IRequestHandler<CreateNovedadTecnicaCommand, Ent.NovedadesTecnicas>,
    IRequestHandler<UpdateNovedadTecnicaCommand, bool>,
    IRequestHandler<DeleteNovedadTecnicaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public NovedadesTecnicasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.NovedadesTecnicas>> Handle(GetAllNovedadesTecnicasQuery r, CancellationToken ct)
        => await _context.NovedadesTecnicas.ToListAsync(ct);

    public async Task<Ent.NovedadesTecnicas?> Handle(GetNovedadTecnicaByIdQuery r, CancellationToken ct)
        => await _context.NovedadesTecnicas.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.NovedadesTecnicas> Handle(CreateNovedadTecnicaCommand r, CancellationToken ct)
    {
        _context.NovedadesTecnicas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateNovedadTecnicaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.NovedadTecnicaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteNovedadTecnicaCommand r, CancellationToken ct)
    {
        var item = await _context.NovedadesTecnicas.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.NovedadesTecnicas.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
