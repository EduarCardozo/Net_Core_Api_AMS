using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.UsuariosRoles;

public record GetAllUsuariosRolesQuery : IRequest<IEnumerable<Ent.UsuariosRoles>>;
public record CreateUsuarioRolCommand(Ent.UsuariosRoles Item) : IRequest<Ent.UsuariosRoles>;

public class UsuariosRolesHandlers :
    IRequestHandler<GetAllUsuariosRolesQuery, IEnumerable<Ent.UsuariosRoles>>,
    IRequestHandler<CreateUsuarioRolCommand, Ent.UsuariosRoles>
{
    private readonly AmsFlotDbContext _context;
    public UsuariosRolesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.UsuariosRoles>> Handle(GetAllUsuariosRolesQuery r, CancellationToken ct)
        => await _context.UsuariosRoles.ToListAsync(ct);

    public async Task<Ent.UsuariosRoles> Handle(CreateUsuarioRolCommand r, CancellationToken ct)
    {
        _context.UsuariosRoles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
