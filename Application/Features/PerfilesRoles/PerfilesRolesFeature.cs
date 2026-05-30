using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PerfilesRoles;

public record GetAllPerfilesRolesQuery : IRequest<IEnumerable<Ent.PerfilesRoles>>;
public record CreatePerfilRolCommand(Ent.PerfilesRoles Item) : IRequest<Ent.PerfilesRoles>;

public class PerfilesRolesHandlers :
    IRequestHandler<GetAllPerfilesRolesQuery, IEnumerable<Ent.PerfilesRoles>>,
    IRequestHandler<CreatePerfilRolCommand, Ent.PerfilesRoles>
{
    private readonly AmsFlotDbContext _context;
    public PerfilesRolesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PerfilesRoles>> Handle(GetAllPerfilesRolesQuery r, CancellationToken ct)
        => await _context.PerfilesRoles.ToListAsync(ct);

    public async Task<Ent.PerfilesRoles> Handle(CreatePerfilRolCommand r, CancellationToken ct)
    {
        _context.PerfilesRoles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
