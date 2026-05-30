using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.URLTargetsRoles;

public record GetAllURLTargetsRolesQuery : IRequest<IEnumerable<Ent.URLTargetsRoles>>;
public record CreateURLTargetRolCommand(Ent.URLTargetsRoles Item) : IRequest<Ent.URLTargetsRoles>;

public class URLTargetsRolesHandlers :
    IRequestHandler<GetAllURLTargetsRolesQuery, IEnumerable<Ent.URLTargetsRoles>>,
    IRequestHandler<CreateURLTargetRolCommand, Ent.URLTargetsRoles>
{
    private readonly AmsFlotDbContext _context;
    public URLTargetsRolesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.URLTargetsRoles>> Handle(GetAllURLTargetsRolesQuery r, CancellationToken ct)
        => await _context.URLTargetsRoles.ToListAsync(ct);

    public async Task<Ent.URLTargetsRoles> Handle(CreateURLTargetRolCommand r, CancellationToken ct)
    {
        _context.URLTargetsRoles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
