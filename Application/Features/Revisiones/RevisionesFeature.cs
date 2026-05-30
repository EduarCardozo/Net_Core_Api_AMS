using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Revisiones;

public record GetAllRevisionesQuery : IRequest<IEnumerable<Ent.Revisiones>>;
public record GetRevisionByIdQuery(int Id) : IRequest<Ent.Revisiones?>;
public record CreateRevisionCommand(Ent.Revisiones Item) : IRequest<Ent.Revisiones>;
public record UpdateRevisionCommand(int Id, Ent.Revisiones Item) : IRequest<bool>;
public record DeleteRevisionCommand(int Id) : IRequest<bool>;

public class RevisionesHandlers :
    IRequestHandler<GetAllRevisionesQuery, IEnumerable<Ent.Revisiones>>,
    IRequestHandler<GetRevisionByIdQuery, Ent.Revisiones?>,
    IRequestHandler<CreateRevisionCommand, Ent.Revisiones>,
    IRequestHandler<UpdateRevisionCommand, bool>,
    IRequestHandler<DeleteRevisionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RevisionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Revisiones>> Handle(GetAllRevisionesQuery r, CancellationToken ct)
        => await _context.Revisiones.ToListAsync(ct);

    public async Task<Ent.Revisiones?> Handle(GetRevisionByIdQuery r, CancellationToken ct)
        => await _context.Revisiones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Revisiones> Handle(CreateRevisionCommand r, CancellationToken ct)
    {
        _context.Revisiones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRevisionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.RevisionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRevisionCommand r, CancellationToken ct)
    {
        var item = await _context.Revisiones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Revisiones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
