using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PartesCategorias;

public record GetAllPartesCategoriasQuery : IRequest<IEnumerable<Ent.PartesCategorias>>;
public record GetParteCategoriaByIdQuery(short Id) : IRequest<Ent.PartesCategorias?>;
public record CreateParteCategoriaCommand(Ent.PartesCategorias Item) : IRequest<Ent.PartesCategorias>;
public record UpdateParteCategoriaCommand(short Id, Ent.PartesCategorias Item) : IRequest<bool>;
public record DeleteParteCategoriaCommand(short Id) : IRequest<bool>;

public class PartesCategoriasHandlers :
    IRequestHandler<GetAllPartesCategoriasQuery, IEnumerable<Ent.PartesCategorias>>,
    IRequestHandler<GetParteCategoriaByIdQuery, Ent.PartesCategorias?>,
    IRequestHandler<CreateParteCategoriaCommand, Ent.PartesCategorias>,
    IRequestHandler<UpdateParteCategoriaCommand, bool>,
    IRequestHandler<DeleteParteCategoriaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PartesCategoriasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PartesCategorias>> Handle(GetAllPartesCategoriasQuery r, CancellationToken ct)
        => await _context.PartesCategorias.ToListAsync(ct);

    public async Task<Ent.PartesCategorias?> Handle(GetParteCategoriaByIdQuery r, CancellationToken ct)
        => await _context.PartesCategorias.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PartesCategorias> Handle(CreateParteCategoriaCommand r, CancellationToken ct)
    {
        _context.PartesCategorias.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateParteCategoriaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ParteCategoriaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteParteCategoriaCommand r, CancellationToken ct)
    {
        var item = await _context.PartesCategorias.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PartesCategorias.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
