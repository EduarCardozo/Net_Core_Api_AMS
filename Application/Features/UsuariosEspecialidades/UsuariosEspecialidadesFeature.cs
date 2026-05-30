using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.UsuariosEspecialidades;

public record GetAllUsuariosEspecialidadesQuery : IRequest<IEnumerable<Ent.UsuariosEspecialidades>>;
public record CreateUsuarioEspecialidadCommand(Ent.UsuariosEspecialidades Item) : IRequest<Ent.UsuariosEspecialidades>;

public class UsuariosEspecialidadesHandlers :
    IRequestHandler<GetAllUsuariosEspecialidadesQuery, IEnumerable<Ent.UsuariosEspecialidades>>,
    IRequestHandler<CreateUsuarioEspecialidadCommand, Ent.UsuariosEspecialidades>
{
    private readonly AmsFlotDbContext _context;
    public UsuariosEspecialidadesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.UsuariosEspecialidades>> Handle(GetAllUsuariosEspecialidadesQuery r, CancellationToken ct)
        => await _context.UsuariosEspecialidades.ToListAsync(ct);

    public async Task<Ent.UsuariosEspecialidades> Handle(CreateUsuarioEspecialidadCommand r, CancellationToken ct)
    {
        _context.UsuariosEspecialidades.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
