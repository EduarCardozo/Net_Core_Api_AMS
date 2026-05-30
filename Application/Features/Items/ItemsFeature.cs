using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Items;

public record GetAllItemsQuery : IRequest<IEnumerable<Ent.Items>>;
public record GetItemByIdQuery(int Id) : IRequest<Ent.Items?>;
public record CreateItemCommand(Ent.Items Item) : IRequest<Ent.Items>;
public record UpdateItemCommand(int Id, Ent.Items Item) : IRequest<bool>;
public record DeleteItemCommand(int Id) : IRequest<bool>;

public class ItemsHandlers :
    IRequestHandler<GetAllItemsQuery, IEnumerable<Ent.Items>>,
    IRequestHandler<GetItemByIdQuery, Ent.Items?>,
    IRequestHandler<CreateItemCommand, Ent.Items>,
    IRequestHandler<UpdateItemCommand, bool>,
    IRequestHandler<DeleteItemCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public ItemsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Items>> Handle(GetAllItemsQuery r, CancellationToken ct)
        => await _context.Items.ToListAsync(ct);

    public async Task<Ent.Items?> Handle(GetItemByIdQuery r, CancellationToken ct)
        => await _context.Items.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Items> Handle(CreateItemCommand r, CancellationToken ct)
    {
        _context.Items.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateItemCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ItemID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteItemCommand r, CancellationToken ct)
    {
        var item = await _context.Items.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Items.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
