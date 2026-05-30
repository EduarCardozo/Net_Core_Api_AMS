using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PartesStockMovimientos;

public record GetAllPartesStockMovimientosQuery : IRequest<IEnumerable<Ent.PartesStockMovimientos>>;
public record GetParteStockMovimientoByIdQuery(int Id) : IRequest<Ent.PartesStockMovimientos?>;
public record CreateParteStockMovimientoCommand(Ent.PartesStockMovimientos Item) : IRequest<Ent.PartesStockMovimientos>;
public record UpdateParteStockMovimientoCommand(int Id, Ent.PartesStockMovimientos Item) : IRequest<bool>;
public record DeleteParteStockMovimientoCommand(int Id) : IRequest<bool>;

public class PartesStockMovimientosHandlers :
    IRequestHandler<GetAllPartesStockMovimientosQuery, IEnumerable<Ent.PartesStockMovimientos>>,
    IRequestHandler<GetParteStockMovimientoByIdQuery, Ent.PartesStockMovimientos?>,
    IRequestHandler<CreateParteStockMovimientoCommand, Ent.PartesStockMovimientos>,
    IRequestHandler<UpdateParteStockMovimientoCommand, bool>,
    IRequestHandler<DeleteParteStockMovimientoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PartesStockMovimientosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PartesStockMovimientos>> Handle(GetAllPartesStockMovimientosQuery r, CancellationToken ct)
        => await _context.PartesStockMovimientos.ToListAsync(ct);

    public async Task<Ent.PartesStockMovimientos?> Handle(GetParteStockMovimientoByIdQuery r, CancellationToken ct)
        => await _context.PartesStockMovimientos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PartesStockMovimientos> Handle(CreateParteStockMovimientoCommand r, CancellationToken ct)
    {
        _context.PartesStockMovimientos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateParteStockMovimientoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ParteStockMovimientoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteParteStockMovimientoCommand r, CancellationToken ct)
    {
        var item = await _context.PartesStockMovimientos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PartesStockMovimientos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
