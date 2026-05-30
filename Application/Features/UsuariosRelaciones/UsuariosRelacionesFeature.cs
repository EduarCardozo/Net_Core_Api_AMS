using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.UsuariosRelaciones;

public record GetAllUsuariosRelacionesQuery : IRequest<IEnumerable<Ent.UsuariosRelaciones>>;
public record CreateUsuarioRelacionCommand(Ent.UsuariosRelaciones Item) : IRequest<Ent.UsuariosRelaciones>;

public class UsuariosRelacionesHandlers :
    IRequestHandler<GetAllUsuariosRelacionesQuery, IEnumerable<Ent.UsuariosRelaciones>>,
    IRequestHandler<CreateUsuarioRelacionCommand, Ent.UsuariosRelaciones>
{
    private readonly AmsFlotDbContext _context;
    public UsuariosRelacionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.UsuariosRelaciones>> Handle(GetAllUsuariosRelacionesQuery r, CancellationToken ct)
        => await _context.UsuariosRelaciones.ToListAsync(ct);

    public async Task<Ent.UsuariosRelaciones> Handle(CreateUsuarioRelacionCommand r, CancellationToken ct)
    {
        _context.UsuariosRelaciones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
