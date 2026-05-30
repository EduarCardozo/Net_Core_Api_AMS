using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_Impuestos;

public record GetAllTAX_ImpuestosQuery : IRequest<IEnumerable<Ent.TAX_Impuestos>>;
public record GetTAX_ImpuestoByIdQuery(short Id) : IRequest<Ent.TAX_Impuestos?>;
public record CreateTAX_ImpuestoCommand(Ent.TAX_Impuestos Item) : IRequest<Ent.TAX_Impuestos>;
public record UpdateTAX_ImpuestoCommand(short Id, Ent.TAX_Impuestos Item) : IRequest<bool>;
public record DeleteTAX_ImpuestoCommand(short Id) : IRequest<bool>;

public class TAX_ImpuestosHandlers :
    IRequestHandler<GetAllTAX_ImpuestosQuery, IEnumerable<Ent.TAX_Impuestos>>,
    IRequestHandler<GetTAX_ImpuestoByIdQuery, Ent.TAX_Impuestos?>,
    IRequestHandler<CreateTAX_ImpuestoCommand, Ent.TAX_Impuestos>,
    IRequestHandler<UpdateTAX_ImpuestoCommand, bool>,
    IRequestHandler<DeleteTAX_ImpuestoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_ImpuestosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_Impuestos>> Handle(GetAllTAX_ImpuestosQuery r, CancellationToken ct)
        => await _context.TAX_Impuestos.ToListAsync(ct);

    public async Task<Ent.TAX_Impuestos?> Handle(GetTAX_ImpuestoByIdQuery r, CancellationToken ct)
        => await _context.TAX_Impuestos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_Impuestos> Handle(CreateTAX_ImpuestoCommand r, CancellationToken ct)
    {
        _context.TAX_Impuestos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_ImpuestoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ImpuestoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_ImpuestoCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_Impuestos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_Impuestos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
