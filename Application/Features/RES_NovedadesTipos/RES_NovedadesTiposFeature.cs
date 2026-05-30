using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_NovedadesTipos;

public record GetAllRES_NovedadesTiposQuery : IRequest<IEnumerable<Ent.RES_NovedadesTipos>>;
public record GetRES_NovedadTipoByIdQuery(int Id) : IRequest<Ent.RES_NovedadesTipos?>;
public record CreateRES_NovedadTipoCommand(Ent.RES_NovedadesTipos Item) : IRequest<Ent.RES_NovedadesTipos>;
public record UpdateRES_NovedadTipoCommand(int Id, Ent.RES_NovedadesTipos Item) : IRequest<bool>;
public record DeleteRES_NovedadTipoCommand(int Id) : IRequest<bool>;

public class RES_NovedadesTiposHandlers :
    IRequestHandler<GetAllRES_NovedadesTiposQuery, IEnumerable<Ent.RES_NovedadesTipos>>,
    IRequestHandler<GetRES_NovedadTipoByIdQuery, Ent.RES_NovedadesTipos?>,
    IRequestHandler<CreateRES_NovedadTipoCommand, Ent.RES_NovedadesTipos>,
    IRequestHandler<UpdateRES_NovedadTipoCommand, bool>,
    IRequestHandler<DeleteRES_NovedadTipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_NovedadesTiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_NovedadesTipos>> Handle(GetAllRES_NovedadesTiposQuery r, CancellationToken ct)
        => await _context.RES_NovedadesTipos.ToListAsync(ct);

    public async Task<Ent.RES_NovedadesTipos?> Handle(GetRES_NovedadTipoByIdQuery r, CancellationToken ct)
        => await _context.RES_NovedadesTipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_NovedadesTipos> Handle(CreateRES_NovedadTipoCommand r, CancellationToken ct)
    {
        _context.RES_NovedadesTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_NovedadTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.NovedadTipoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_NovedadTipoCommand r, CancellationToken ct)
    {
        var item = await _context.RES_NovedadesTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_NovedadesTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
