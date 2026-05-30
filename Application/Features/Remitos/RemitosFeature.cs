using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Remitos;

public record GetAllRemitosQuery : IRequest<IEnumerable<Ent.Remitos>>;
public record GetRemitoByIdQuery(int Id) : IRequest<Ent.Remitos?>;
public record CreateRemitoCommand(Ent.Remitos Item) : IRequest<Ent.Remitos>;
public record UpdateRemitoCommand(int Id, Ent.Remitos Item) : IRequest<bool>;
public record DeleteRemitoCommand(int Id) : IRequest<bool>;

public class RemitosHandlers :
    IRequestHandler<GetAllRemitosQuery, IEnumerable<Ent.Remitos>>,
    IRequestHandler<GetRemitoByIdQuery, Ent.Remitos?>,
    IRequestHandler<CreateRemitoCommand, Ent.Remitos>,
    IRequestHandler<UpdateRemitoCommand, bool>,
    IRequestHandler<DeleteRemitoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RemitosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Remitos>> Handle(GetAllRemitosQuery r, CancellationToken ct)
        => await _context.Remitos.ToListAsync(ct);

    public async Task<Ent.Remitos?> Handle(GetRemitoByIdQuery r, CancellationToken ct)
        => await _context.Remitos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Remitos> Handle(CreateRemitoCommand r, CancellationToken ct)
    {
        _context.Remitos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRemitoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.RemitoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRemitoCommand r, CancellationToken ct)
    {
        var item = await _context.Remitos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Remitos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
