using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.S_PrestadoresCategorias;

public record GetAllS_PrestadoresCategoriasQuery : IRequest<IEnumerable<Ent.S_PrestadoresCategorias>>;
public record GetS_PrestadorCategoriaByIdQuery(short Id) : IRequest<Ent.S_PrestadoresCategorias?>;
public record CreateS_PrestadorCategoriaCommand(Ent.S_PrestadoresCategorias Item) : IRequest<Ent.S_PrestadoresCategorias>;
public record UpdateS_PrestadorCategoriaCommand(short Id, Ent.S_PrestadoresCategorias Item) : IRequest<bool>;
public record DeleteS_PrestadorCategoriaCommand(short Id) : IRequest<bool>;

public class S_PrestadoresCategoriasHandlers :
    IRequestHandler<GetAllS_PrestadoresCategoriasQuery, IEnumerable<Ent.S_PrestadoresCategorias>>,
    IRequestHandler<GetS_PrestadorCategoriaByIdQuery, Ent.S_PrestadoresCategorias?>,
    IRequestHandler<CreateS_PrestadorCategoriaCommand, Ent.S_PrestadoresCategorias>,
    IRequestHandler<UpdateS_PrestadorCategoriaCommand, bool>,
    IRequestHandler<DeleteS_PrestadorCategoriaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public S_PrestadoresCategoriasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.S_PrestadoresCategorias>> Handle(GetAllS_PrestadoresCategoriasQuery r, CancellationToken ct)
        => await _context.S_PrestadoresCategorias.ToListAsync(ct);

    public async Task<Ent.S_PrestadoresCategorias?> Handle(GetS_PrestadorCategoriaByIdQuery r, CancellationToken ct)
        => await _context.S_PrestadoresCategorias.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.S_PrestadoresCategorias> Handle(CreateS_PrestadorCategoriaCommand r, CancellationToken ct)
    {
        _context.S_PrestadoresCategorias.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateS_PrestadorCategoriaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PrestadorCategoriaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteS_PrestadorCategoriaCommand r, CancellationToken ct)
    {
        var item = await _context.S_PrestadoresCategorias.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.S_PrestadoresCategorias.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
