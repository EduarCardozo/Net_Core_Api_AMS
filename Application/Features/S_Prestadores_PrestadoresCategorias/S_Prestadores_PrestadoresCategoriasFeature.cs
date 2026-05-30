using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.S_Prestadores_PrestadoresCategorias;

public record GetAllS_Prestadores_PrestadoresCategoriasQuery : IRequest<IEnumerable<Ent.S_Prestadores_PrestadoresCategorias>>;
public record CreateS_Prestador_PrestadorCategoriaCommand(Ent.S_Prestadores_PrestadoresCategorias Item) : IRequest<Ent.S_Prestadores_PrestadoresCategorias>;

public class S_Prestadores_PrestadoresCategoriasHandlers :
    IRequestHandler<GetAllS_Prestadores_PrestadoresCategoriasQuery, IEnumerable<Ent.S_Prestadores_PrestadoresCategorias>>,
    IRequestHandler<CreateS_Prestador_PrestadorCategoriaCommand, Ent.S_Prestadores_PrestadoresCategorias>
{
    private readonly AmsFlotDbContext _context;
    public S_Prestadores_PrestadoresCategoriasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.S_Prestadores_PrestadoresCategorias>> Handle(GetAllS_Prestadores_PrestadoresCategoriasQuery r, CancellationToken ct)
        => await _context.S_Prestadores_PrestadoresCategorias.ToListAsync(ct);

    public async Task<Ent.S_Prestadores_PrestadoresCategorias> Handle(CreateS_Prestador_PrestadorCategoriaCommand r, CancellationToken ct)
    {
        _context.S_Prestadores_PrestadoresCategorias.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
