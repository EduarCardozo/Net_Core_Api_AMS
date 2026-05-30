using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.EquiposPartesItems;

public record GetAllEquiposPartesItemsQuery : IRequest<IEnumerable<Ent.EquiposPartesItems>>;
public record CreateEquipoParteItemCommand(Ent.EquiposPartesItems Item) : IRequest<Ent.EquiposPartesItems>;

public class EquiposPartesItemsHandlers :
    IRequestHandler<GetAllEquiposPartesItemsQuery, IEnumerable<Ent.EquiposPartesItems>>,
    IRequestHandler<CreateEquipoParteItemCommand, Ent.EquiposPartesItems>
{
    private readonly AmsFlotDbContext _context;
    public EquiposPartesItemsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.EquiposPartesItems>> Handle(GetAllEquiposPartesItemsQuery r, CancellationToken ct)
        => await _context.EquiposPartesItems.ToListAsync(ct);

    public async Task<Ent.EquiposPartesItems> Handle(CreateEquipoParteItemCommand r, CancellationToken ct)
    {
        _context.EquiposPartesItems.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
