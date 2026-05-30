using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.G_DireccionesOwners;

public record GetAllG_DireccionesOwnersQuery : IRequest<IEnumerable<Ent.G_DireccionesOwners>>;
public record CreateG_DireccionOwnerCommand(Ent.G_DireccionesOwners Item) : IRequest<Ent.G_DireccionesOwners>;

public class G_DireccionesOwnersHandlers :
    IRequestHandler<GetAllG_DireccionesOwnersQuery, IEnumerable<Ent.G_DireccionesOwners>>,
    IRequestHandler<CreateG_DireccionOwnerCommand, Ent.G_DireccionesOwners>
{
    private readonly AmsFlotDbContext _context;
    public G_DireccionesOwnersHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.G_DireccionesOwners>> Handle(GetAllG_DireccionesOwnersQuery r, CancellationToken ct)
        => await _context.G_DireccionesOwners.ToListAsync(ct);

    public async Task<Ent.G_DireccionesOwners> Handle(CreateG_DireccionOwnerCommand r, CancellationToken ct)
    {
        _context.G_DireccionesOwners.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
