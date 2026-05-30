using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.OrdenesTrabajoPartesItems;

public record GetAllOrdenesTrabajoPartesItemsQuery : IRequest<IEnumerable<Ent.OrdenesTrabajoPartesItems>>;
public record CreateOrdenTrabajoParteItemCommand(Ent.OrdenesTrabajoPartesItems Item) : IRequest<Ent.OrdenesTrabajoPartesItems>;

public class OrdenesTrabajoPartesItemsHandlers :
    IRequestHandler<GetAllOrdenesTrabajoPartesItemsQuery, IEnumerable<Ent.OrdenesTrabajoPartesItems>>,
    IRequestHandler<CreateOrdenTrabajoParteItemCommand, Ent.OrdenesTrabajoPartesItems>
{
    private readonly AmsFlotDbContext _context;
    public OrdenesTrabajoPartesItemsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.OrdenesTrabajoPartesItems>> Handle(GetAllOrdenesTrabajoPartesItemsQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajoPartesItems.ToListAsync(ct);

    public async Task<Ent.OrdenesTrabajoPartesItems> Handle(CreateOrdenTrabajoParteItemCommand r, CancellationToken ct)
    {
        _context.OrdenesTrabajoPartesItems.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
