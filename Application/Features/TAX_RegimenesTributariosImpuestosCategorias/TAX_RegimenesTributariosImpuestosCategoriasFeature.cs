using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_RegimenesTributariosImpuestosCategorias;

public record GetAllTAX_RegimenesTributariosImpuestosCategoriasQuery : IRequest<IEnumerable<Ent.TAX_RegimenesTributariosImpuestosCategorias>>;
public record GetTAX_RegimenTributarioImpuestoCategoriaByIdQuery(int Id) : IRequest<Ent.TAX_RegimenesTributariosImpuestosCategorias?>;
public record CreateTAX_RegimenTributarioImpuestoCategoriaCommand(Ent.TAX_RegimenesTributariosImpuestosCategorias Item) : IRequest<Ent.TAX_RegimenesTributariosImpuestosCategorias>;
public record UpdateTAX_RegimenTributarioImpuestoCategoriaCommand(int Id, Ent.TAX_RegimenesTributariosImpuestosCategorias Item) : IRequest<bool>;
public record DeleteTAX_RegimenTributarioImpuestoCategoriaCommand(int Id) : IRequest<bool>;

public class TAX_RegimenesTributariosImpuestosCategoriasHandlers :
    IRequestHandler<GetAllTAX_RegimenesTributariosImpuestosCategoriasQuery, IEnumerable<Ent.TAX_RegimenesTributariosImpuestosCategorias>>,
    IRequestHandler<GetTAX_RegimenTributarioImpuestoCategoriaByIdQuery, Ent.TAX_RegimenesTributariosImpuestosCategorias?>,
    IRequestHandler<CreateTAX_RegimenTributarioImpuestoCategoriaCommand, Ent.TAX_RegimenesTributariosImpuestosCategorias>,
    IRequestHandler<UpdateTAX_RegimenTributarioImpuestoCategoriaCommand, bool>,
    IRequestHandler<DeleteTAX_RegimenTributarioImpuestoCategoriaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_RegimenesTributariosImpuestosCategoriasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_RegimenesTributariosImpuestosCategorias>> Handle(GetAllTAX_RegimenesTributariosImpuestosCategoriasQuery r, CancellationToken ct)
        => await _context.TAX_RegimenesTributariosImpuestosCategorias.ToListAsync(ct);

    public async Task<Ent.TAX_RegimenesTributariosImpuestosCategorias?> Handle(GetTAX_RegimenTributarioImpuestoCategoriaByIdQuery r, CancellationToken ct)
        => await _context.TAX_RegimenesTributariosImpuestosCategorias.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_RegimenesTributariosImpuestosCategorias> Handle(CreateTAX_RegimenTributarioImpuestoCategoriaCommand r, CancellationToken ct)
    {
        _context.TAX_RegimenesTributariosImpuestosCategorias.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_RegimenTributarioImpuestoCategoriaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.RegimenTributarioImpuestoCategoriaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_RegimenTributarioImpuestoCategoriaCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_RegimenesTributariosImpuestosCategorias.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_RegimenesTributariosImpuestosCategorias.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
