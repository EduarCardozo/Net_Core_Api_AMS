using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.S_ServiciosPrestadores;

public record GetAllS_ServiciosPrestadoresQuery : IRequest<IEnumerable<Ent.S_ServiciosPrestadores>>;
public record GetS_ServicioPrestadorByIdQuery(int Id) : IRequest<Ent.S_ServiciosPrestadores?>;
public record CreateS_ServicioPrestadorCommand(Ent.S_ServiciosPrestadores Item) : IRequest<Ent.S_ServiciosPrestadores>;
public record UpdateS_ServicioPrestadorCommand(int Id, Ent.S_ServiciosPrestadores Item) : IRequest<bool>;
public record DeleteS_ServicioPrestadorCommand(int Id) : IRequest<bool>;

public class S_ServiciosPrestadoresHandlers :
    IRequestHandler<GetAllS_ServiciosPrestadoresQuery, IEnumerable<Ent.S_ServiciosPrestadores>>,
    IRequestHandler<GetS_ServicioPrestadorByIdQuery, Ent.S_ServiciosPrestadores?>,
    IRequestHandler<CreateS_ServicioPrestadorCommand, Ent.S_ServiciosPrestadores>,
    IRequestHandler<UpdateS_ServicioPrestadorCommand, bool>,
    IRequestHandler<DeleteS_ServicioPrestadorCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public S_ServiciosPrestadoresHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.S_ServiciosPrestadores>> Handle(GetAllS_ServiciosPrestadoresQuery r, CancellationToken ct)
        => await _context.S_ServiciosPrestadores.ToListAsync(ct);

    public async Task<Ent.S_ServiciosPrestadores?> Handle(GetS_ServicioPrestadorByIdQuery r, CancellationToken ct)
        => await _context.S_ServiciosPrestadores.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.S_ServiciosPrestadores> Handle(CreateS_ServicioPrestadorCommand r, CancellationToken ct)
    {
        _context.S_ServiciosPrestadores.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateS_ServicioPrestadorCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ServicioPrestadorID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteS_ServicioPrestadorCommand r, CancellationToken ct)
    {
        var item = await _context.S_ServiciosPrestadores.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.S_ServiciosPrestadores.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
