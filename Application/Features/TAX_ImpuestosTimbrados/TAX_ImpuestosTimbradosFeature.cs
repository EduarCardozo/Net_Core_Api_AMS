using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_ImpuestosTimbrados;

public record GetAllTAX_ImpuestosTimbradosQuery : IRequest<IEnumerable<Ent.TAX_ImpuestosTimbrados>>;
public record GetTAX_ImpuestoTimbradoByIdQuery(int Id) : IRequest<Ent.TAX_ImpuestosTimbrados?>;
public record CreateTAX_ImpuestoTimbradoCommand(Ent.TAX_ImpuestosTimbrados Item) : IRequest<Ent.TAX_ImpuestosTimbrados>;
public record UpdateTAX_ImpuestoTimbradoCommand(int Id, Ent.TAX_ImpuestosTimbrados Item) : IRequest<bool>;
public record DeleteTAX_ImpuestoTimbradoCommand(int Id) : IRequest<bool>;

public class TAX_ImpuestosTimbradosHandlers :
    IRequestHandler<GetAllTAX_ImpuestosTimbradosQuery, IEnumerable<Ent.TAX_ImpuestosTimbrados>>,
    IRequestHandler<GetTAX_ImpuestoTimbradoByIdQuery, Ent.TAX_ImpuestosTimbrados?>,
    IRequestHandler<CreateTAX_ImpuestoTimbradoCommand, Ent.TAX_ImpuestosTimbrados>,
    IRequestHandler<UpdateTAX_ImpuestoTimbradoCommand, bool>,
    IRequestHandler<DeleteTAX_ImpuestoTimbradoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_ImpuestosTimbradosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_ImpuestosTimbrados>> Handle(GetAllTAX_ImpuestosTimbradosQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosTimbrados.ToListAsync(ct);

    public async Task<Ent.TAX_ImpuestosTimbrados?> Handle(GetTAX_ImpuestoTimbradoByIdQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosTimbrados.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_ImpuestosTimbrados> Handle(CreateTAX_ImpuestoTimbradoCommand r, CancellationToken ct)
    {
        _context.TAX_ImpuestosTimbrados.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_ImpuestoTimbradoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ImpuestoTimbradoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_ImpuestoTimbradoCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_ImpuestosTimbrados.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_ImpuestosTimbrados.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
