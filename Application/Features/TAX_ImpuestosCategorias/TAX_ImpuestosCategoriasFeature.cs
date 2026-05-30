using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_ImpuestosCategorias;

public record GetAllTAX_ImpuestosCategoriasQuery : IRequest<IEnumerable<Ent.TAX_ImpuestosCategorias>>;
public record GetTAX_ImpuestoCategoriaByIdQuery(int Id) : IRequest<Ent.TAX_ImpuestosCategorias?>;
public record CreateTAX_ImpuestoCategoriaCommand(Ent.TAX_ImpuestosCategorias Item) : IRequest<Ent.TAX_ImpuestosCategorias>;
public record UpdateTAX_ImpuestoCategoriaCommand(int Id, Ent.TAX_ImpuestosCategorias Item) : IRequest<bool>;
public record DeleteTAX_ImpuestoCategoriaCommand(int Id) : IRequest<bool>;

public class TAX_ImpuestosCategoriasHandlers :
    IRequestHandler<GetAllTAX_ImpuestosCategoriasQuery, IEnumerable<Ent.TAX_ImpuestosCategorias>>,
    IRequestHandler<GetTAX_ImpuestoCategoriaByIdQuery, Ent.TAX_ImpuestosCategorias?>,
    IRequestHandler<CreateTAX_ImpuestoCategoriaCommand, Ent.TAX_ImpuestosCategorias>,
    IRequestHandler<UpdateTAX_ImpuestoCategoriaCommand, bool>,
    IRequestHandler<DeleteTAX_ImpuestoCategoriaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_ImpuestosCategoriasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_ImpuestosCategorias>> Handle(GetAllTAX_ImpuestosCategoriasQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosCategorias.ToListAsync(ct);

    public async Task<Ent.TAX_ImpuestosCategorias?> Handle(GetTAX_ImpuestoCategoriaByIdQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosCategorias.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_ImpuestosCategorias> Handle(CreateTAX_ImpuestoCategoriaCommand r, CancellationToken ct)
    {
        _context.TAX_ImpuestosCategorias.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_ImpuestoCategoriaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ImpuestoCategoriaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_ImpuestoCategoriaCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_ImpuestosCategorias.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_ImpuestosCategorias.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
