using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_ImpuestosAlicuotas;

public record GetAllTAX_ImpuestosAlicuotasQuery : IRequest<IEnumerable<Ent.TAX_ImpuestosAlicuotas>>;
public record GetTAX_ImpuestoAlicuotaByIdQuery(int Id) : IRequest<Ent.TAX_ImpuestosAlicuotas?>;
public record CreateTAX_ImpuestoAlicuotaCommand(Ent.TAX_ImpuestosAlicuotas Item) : IRequest<Ent.TAX_ImpuestosAlicuotas>;
public record UpdateTAX_ImpuestoAlicuotaCommand(int Id, Ent.TAX_ImpuestosAlicuotas Item) : IRequest<bool>;
public record DeleteTAX_ImpuestoAlicuotaCommand(int Id) : IRequest<bool>;

public class TAX_ImpuestosAlicuotasHandlers :
    IRequestHandler<GetAllTAX_ImpuestosAlicuotasQuery, IEnumerable<Ent.TAX_ImpuestosAlicuotas>>,
    IRequestHandler<GetTAX_ImpuestoAlicuotaByIdQuery, Ent.TAX_ImpuestosAlicuotas?>,
    IRequestHandler<CreateTAX_ImpuestoAlicuotaCommand, Ent.TAX_ImpuestosAlicuotas>,
    IRequestHandler<UpdateTAX_ImpuestoAlicuotaCommand, bool>,
    IRequestHandler<DeleteTAX_ImpuestoAlicuotaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_ImpuestosAlicuotasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_ImpuestosAlicuotas>> Handle(GetAllTAX_ImpuestosAlicuotasQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosAlicuotas.ToListAsync(ct);

    public async Task<Ent.TAX_ImpuestosAlicuotas?> Handle(GetTAX_ImpuestoAlicuotaByIdQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosAlicuotas.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_ImpuestosAlicuotas> Handle(CreateTAX_ImpuestoAlicuotaCommand r, CancellationToken ct)
    {
        _context.TAX_ImpuestosAlicuotas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_ImpuestoAlicuotaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ImpuestoAlicuotaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_ImpuestoAlicuotaCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_ImpuestosAlicuotas.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_ImpuestosAlicuotas.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
