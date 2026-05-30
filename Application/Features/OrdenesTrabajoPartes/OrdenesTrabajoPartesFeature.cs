using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.OrdenesTrabajoPartes;

public record GetAllOrdenesTrabajoPartesQuery : IRequest<IEnumerable<Ent.OrdenesTrabajoPartes>>;
public record CreateOrdenTrabajoParteCommand(Ent.OrdenesTrabajoPartes Item) : IRequest<Ent.OrdenesTrabajoPartes>;

public class OrdenesTrabajoPartesHandlers :
    IRequestHandler<GetAllOrdenesTrabajoPartesQuery, IEnumerable<Ent.OrdenesTrabajoPartes>>,
    IRequestHandler<CreateOrdenTrabajoParteCommand, Ent.OrdenesTrabajoPartes>
{
    private readonly AmsFlotDbContext _context;
    public OrdenesTrabajoPartesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.OrdenesTrabajoPartes>> Handle(GetAllOrdenesTrabajoPartesQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajoPartes.ToListAsync(ct);

    public async Task<Ent.OrdenesTrabajoPartes> Handle(CreateOrdenTrabajoParteCommand r, CancellationToken ct)
    {
        _context.OrdenesTrabajoPartes.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
