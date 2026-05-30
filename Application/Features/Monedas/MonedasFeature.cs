using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Monedas;

public record GetAllMonedasQuery : IRequest<IEnumerable<Ent.Monedas>>;
public record GetMonedaByIdQuery(short Id) : IRequest<Ent.Monedas?>;
public record CreateMonedaCommand(Ent.Monedas Item) : IRequest<Ent.Monedas>;
public record UpdateMonedaCommand(short Id, Ent.Monedas Item) : IRequest<bool>;
public record DeleteMonedaCommand(short Id) : IRequest<bool>;

public class MonedasHandlers :
    IRequestHandler<GetAllMonedasQuery, IEnumerable<Ent.Monedas>>,
    IRequestHandler<GetMonedaByIdQuery, Ent.Monedas?>,
    IRequestHandler<CreateMonedaCommand, Ent.Monedas>,
    IRequestHandler<UpdateMonedaCommand, bool>,
    IRequestHandler<DeleteMonedaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public MonedasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Monedas>> Handle(GetAllMonedasQuery r, CancellationToken ct)
        => await _context.Monedas.ToListAsync(ct);

    public async Task<Ent.Monedas?> Handle(GetMonedaByIdQuery r, CancellationToken ct)
        => await _context.Monedas.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Monedas> Handle(CreateMonedaCommand r, CancellationToken ct)
    {
        _context.Monedas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateMonedaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.Id) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteMonedaCommand r, CancellationToken ct)
    {
        var item = await _context.Monedas.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Monedas.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
