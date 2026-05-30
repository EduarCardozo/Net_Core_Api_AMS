using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.S_Prestadores;

public record GetAllS_PrestadoresQuery : IRequest<IEnumerable<Ent.S_Prestadores>>;
public record GetS_PrestadorByIdQuery(int Id) : IRequest<Ent.S_Prestadores?>;
public record CreateS_PrestadorCommand(Ent.S_Prestadores Item) : IRequest<Ent.S_Prestadores>;
public record UpdateS_PrestadorCommand(int Id, Ent.S_Prestadores Item) : IRequest<bool>;
public record DeleteS_PrestadorCommand(int Id) : IRequest<bool>;

public class S_PrestadoresHandlers :
    IRequestHandler<GetAllS_PrestadoresQuery, IEnumerable<Ent.S_Prestadores>>,
    IRequestHandler<GetS_PrestadorByIdQuery, Ent.S_Prestadores?>,
    IRequestHandler<CreateS_PrestadorCommand, Ent.S_Prestadores>,
    IRequestHandler<UpdateS_PrestadorCommand, bool>,
    IRequestHandler<DeleteS_PrestadorCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public S_PrestadoresHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.S_Prestadores>> Handle(GetAllS_PrestadoresQuery r, CancellationToken ct)
        => await _context.S_Prestadores.ToListAsync(ct);

    public async Task<Ent.S_Prestadores?> Handle(GetS_PrestadorByIdQuery r, CancellationToken ct)
        => await _context.S_Prestadores.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.S_Prestadores> Handle(CreateS_PrestadorCommand r, CancellationToken ct)
    {
        _context.S_Prestadores.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateS_PrestadorCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PrestadorID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteS_PrestadorCommand r, CancellationToken ct)
    {
        var item = await _context.S_Prestadores.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.S_Prestadores.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
