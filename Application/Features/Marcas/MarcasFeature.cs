using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Marcas;

public record GetAllMarcasQuery : IRequest<IEnumerable<Ent.Marcas>>;
public record GetMarcaByIdQuery(short Id) : IRequest<Ent.Marcas?>;
public record CreateMarcaCommand(Ent.Marcas Item) : IRequest<Ent.Marcas>;
public record UpdateMarcaCommand(short Id, Ent.Marcas Item) : IRequest<bool>;
public record DeleteMarcaCommand(short Id) : IRequest<bool>;

public class MarcasHandlers :
    IRequestHandler<GetAllMarcasQuery, IEnumerable<Ent.Marcas>>,
    IRequestHandler<GetMarcaByIdQuery, Ent.Marcas?>,
    IRequestHandler<CreateMarcaCommand, Ent.Marcas>,
    IRequestHandler<UpdateMarcaCommand, bool>,
    IRequestHandler<DeleteMarcaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public MarcasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Marcas>> Handle(GetAllMarcasQuery r, CancellationToken ct)
        => await _context.Marcas.ToListAsync(ct);

    public async Task<Ent.Marcas?> Handle(GetMarcaByIdQuery r, CancellationToken ct)
        => await _context.Marcas.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Marcas> Handle(CreateMarcaCommand r, CancellationToken ct)
    {
        _context.Marcas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateMarcaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.MarcaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteMarcaCommand r, CancellationToken ct)
    {
        var item = await _context.Marcas.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Marcas.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
