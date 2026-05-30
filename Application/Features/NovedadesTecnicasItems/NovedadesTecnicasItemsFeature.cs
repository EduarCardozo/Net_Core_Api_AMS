using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.NovedadesTecnicasItems;

public record GetAllNovedadesTecnicasItemsQuery : IRequest<IEnumerable<Ent.NovedadesTecnicasItems>>;
public record CreateNovedadTecnicaItemCommand(Ent.NovedadesTecnicasItems Item) : IRequest<Ent.NovedadesTecnicasItems>;

public class NovedadesTecnicasItemsHandlers :
    IRequestHandler<GetAllNovedadesTecnicasItemsQuery, IEnumerable<Ent.NovedadesTecnicasItems>>,
    IRequestHandler<CreateNovedadTecnicaItemCommand, Ent.NovedadesTecnicasItems>
{
    private readonly AmsFlotDbContext _context;
    public NovedadesTecnicasItemsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.NovedadesTecnicasItems>> Handle(GetAllNovedadesTecnicasItemsQuery r, CancellationToken ct)
        => await _context.NovedadesTecnicasItems.ToListAsync(ct);

    public async Task<Ent.NovedadesTecnicasItems> Handle(CreateNovedadTecnicaItemCommand r, CancellationToken ct)
    {
        _context.NovedadesTecnicasItems.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
