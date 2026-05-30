using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PartesStocks;

public record GetAllPartesStocksQuery : IRequest<IEnumerable<Ent.PartesStocks>>;
public record GetParteStockByIdQuery(int Id) : IRequest<Ent.PartesStocks?>;
public record CreateParteStockCommand(Ent.PartesStocks Item) : IRequest<Ent.PartesStocks>;
public record UpdateParteStockCommand(int Id, Ent.PartesStocks Item) : IRequest<bool>;
public record DeleteParteStockCommand(int Id) : IRequest<bool>;

public class PartesStocksHandlers :
    IRequestHandler<GetAllPartesStocksQuery, IEnumerable<Ent.PartesStocks>>,
    IRequestHandler<GetParteStockByIdQuery, Ent.PartesStocks?>,
    IRequestHandler<CreateParteStockCommand, Ent.PartesStocks>,
    IRequestHandler<UpdateParteStockCommand, bool>,
    IRequestHandler<DeleteParteStockCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PartesStocksHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PartesStocks>> Handle(GetAllPartesStocksQuery r, CancellationToken ct)
        => await _context.PartesStocks.ToListAsync(ct);

    public async Task<Ent.PartesStocks?> Handle(GetParteStockByIdQuery r, CancellationToken ct)
        => await _context.PartesStocks.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PartesStocks> Handle(CreateParteStockCommand r, CancellationToken ct)
    {
        _context.PartesStocks.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateParteStockCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ParteStockID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteParteStockCommand r, CancellationToken ct)
    {
        var item = await _context.PartesStocks.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PartesStocks.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
