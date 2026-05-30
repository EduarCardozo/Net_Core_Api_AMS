using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.S_ServiciosCategorias;

public record GetAllS_ServiciosCategoriasQuery : IRequest<IEnumerable<Ent.S_ServiciosCategorias>>;
public record GetS_ServicioCategoriaByIdQuery(int Id) : IRequest<Ent.S_ServiciosCategorias?>;
public record CreateS_ServicioCategoriaCommand(Ent.S_ServiciosCategorias Item) : IRequest<Ent.S_ServiciosCategorias>;
public record UpdateS_ServicioCategoriaCommand(int Id, Ent.S_ServiciosCategorias Item) : IRequest<bool>;
public record DeleteS_ServicioCategoriaCommand(int Id) : IRequest<bool>;

public class S_ServiciosCategoriasHandlers :
    IRequestHandler<GetAllS_ServiciosCategoriasQuery, IEnumerable<Ent.S_ServiciosCategorias>>,
    IRequestHandler<GetS_ServicioCategoriaByIdQuery, Ent.S_ServiciosCategorias?>,
    IRequestHandler<CreateS_ServicioCategoriaCommand, Ent.S_ServiciosCategorias>,
    IRequestHandler<UpdateS_ServicioCategoriaCommand, bool>,
    IRequestHandler<DeleteS_ServicioCategoriaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public S_ServiciosCategoriasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.S_ServiciosCategorias>> Handle(GetAllS_ServiciosCategoriasQuery r, CancellationToken ct)
        => await _context.S_ServiciosCategorias.ToListAsync(ct);

    public async Task<Ent.S_ServiciosCategorias?> Handle(GetS_ServicioCategoriaByIdQuery r, CancellationToken ct)
        => await _context.S_ServiciosCategorias.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.S_ServiciosCategorias> Handle(CreateS_ServicioCategoriaCommand r, CancellationToken ct)
    {
        _context.S_ServiciosCategorias.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateS_ServicioCategoriaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.S_ServicioCategoriaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteS_ServicioCategoriaCommand r, CancellationToken ct)
    {
        var item = await _context.S_ServiciosCategorias.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.S_ServiciosCategorias.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
