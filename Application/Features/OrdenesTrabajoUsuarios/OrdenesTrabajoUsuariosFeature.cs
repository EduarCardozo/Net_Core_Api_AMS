using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.OrdenesTrabajoUsuarios;

public record GetAllOrdenesTrabajoUsuariosQuery : IRequest<IEnumerable<Ent.OrdenesTrabajoUsuarios>>;
public record CreateOrdenTrabajoUsuarioCommand(Ent.OrdenesTrabajoUsuarios Item) : IRequest<Ent.OrdenesTrabajoUsuarios>;

public class OrdenesTrabajoUsuariosHandlers :
    IRequestHandler<GetAllOrdenesTrabajoUsuariosQuery, IEnumerable<Ent.OrdenesTrabajoUsuarios>>,
    IRequestHandler<CreateOrdenTrabajoUsuarioCommand, Ent.OrdenesTrabajoUsuarios>
{
    private readonly AmsFlotDbContext _context;
    public OrdenesTrabajoUsuariosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.OrdenesTrabajoUsuarios>> Handle(GetAllOrdenesTrabajoUsuariosQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajoUsuarios.ToListAsync(ct);

    public async Task<Ent.OrdenesTrabajoUsuarios> Handle(CreateOrdenTrabajoUsuarioCommand r, CancellationToken ct)
    {
        _context.OrdenesTrabajoUsuarios.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
