using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.UsuariosPerfiles;

public record GetAllUsuariosPerfilesQuery : IRequest<IEnumerable<Ent.UsuariosPerfiles>>;
public record CreateUsuarioPerfilCommand(Ent.UsuariosPerfiles Item) : IRequest<Ent.UsuariosPerfiles>;

public class UsuariosPerfilesHandlers :
    IRequestHandler<GetAllUsuariosPerfilesQuery, IEnumerable<Ent.UsuariosPerfiles>>,
    IRequestHandler<CreateUsuarioPerfilCommand, Ent.UsuariosPerfiles>
{
    private readonly AmsFlotDbContext _context;
    public UsuariosPerfilesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.UsuariosPerfiles>> Handle(GetAllUsuariosPerfilesQuery r, CancellationToken ct)
        => await _context.UsuariosPerfiles.ToListAsync(ct);

    public async Task<Ent.UsuariosPerfiles> Handle(CreateUsuarioPerfilCommand r, CancellationToken ct)
    {
        _context.UsuariosPerfiles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
