using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PerfilesURLTargets;

public record GetAllPerfilesURLTargetsQuery : IRequest<IEnumerable<Ent.PerfilesURLTargets>>;
public record CreatePerfilURLTargetCommand(Ent.PerfilesURLTargets Item) : IRequest<Ent.PerfilesURLTargets>;

public class PerfilesURLTargetsHandlers :
    IRequestHandler<GetAllPerfilesURLTargetsQuery, IEnumerable<Ent.PerfilesURLTargets>>,
    IRequestHandler<CreatePerfilURLTargetCommand, Ent.PerfilesURLTargets>
{
    private readonly AmsFlotDbContext _context;
    public PerfilesURLTargetsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PerfilesURLTargets>> Handle(GetAllPerfilesURLTargetsQuery r, CancellationToken ct)
        => await _context.PerfilesURLTargets.ToListAsync(ct);

    public async Task<Ent.PerfilesURLTargets> Handle(CreatePerfilURLTargetCommand r, CancellationToken ct)
    {
        _context.PerfilesURLTargets.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
