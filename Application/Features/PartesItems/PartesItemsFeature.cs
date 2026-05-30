using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PartesItems;

public record GetAllPartesItemsQuery : IRequest<IEnumerable<Ent.PartesItems>>;
public record GetParteItemByIdQuery(int Id) : IRequest<Ent.PartesItems?>;
public record CreateParteItemCommand(Ent.PartesItems Item) : IRequest<Ent.PartesItems>;
public record UpdateParteItemCommand(int Id, Ent.PartesItems Item) : IRequest<bool>;
public record DeleteParteItemCommand(int Id) : IRequest<bool>;

public class PartesItemsHandlers :
    IRequestHandler<GetAllPartesItemsQuery, IEnumerable<Ent.PartesItems>>,
    IRequestHandler<GetParteItemByIdQuery, Ent.PartesItems?>,
    IRequestHandler<CreateParteItemCommand, Ent.PartesItems>,
    IRequestHandler<UpdateParteItemCommand, bool>,
    IRequestHandler<DeleteParteItemCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PartesItemsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PartesItems>> Handle(GetAllPartesItemsQuery r, CancellationToken ct)
        => await _context.PartesItems.ToListAsync(ct);

    public async Task<Ent.PartesItems?> Handle(GetParteItemByIdQuery r, CancellationToken ct)
        => await _context.PartesItems.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PartesItems> Handle(CreateParteItemCommand r, CancellationToken ct)
    {
        _context.PartesItems.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateParteItemCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ParteItemID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteParteItemCommand r, CancellationToken ct)
    {
        var item = await _context.PartesItems.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PartesItems.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
