using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.S_Servicios;

public record GetAllS_ServiciosQuery : IRequest<IEnumerable<Ent.S_Servicios>>;
public record GetS_ServicioByIdQuery(int Id) : IRequest<Ent.S_Servicios?>;
public record CreateS_ServicioCommand(Ent.S_Servicios Item) : IRequest<Ent.S_Servicios>;
public record UpdateS_ServicioCommand(int Id, Ent.S_Servicios Item) : IRequest<bool>;
public record DeleteS_ServicioCommand(int Id) : IRequest<bool>;

public class S_ServiciosHandlers :
    IRequestHandler<GetAllS_ServiciosQuery, IEnumerable<Ent.S_Servicios>>,
    IRequestHandler<GetS_ServicioByIdQuery, Ent.S_Servicios?>,
    IRequestHandler<CreateS_ServicioCommand, Ent.S_Servicios>,
    IRequestHandler<UpdateS_ServicioCommand, bool>,
    IRequestHandler<DeleteS_ServicioCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public S_ServiciosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.S_Servicios>> Handle(GetAllS_ServiciosQuery r, CancellationToken ct)
        => await _context.S_Servicios.ToListAsync(ct);

    public async Task<Ent.S_Servicios?> Handle(GetS_ServicioByIdQuery r, CancellationToken ct)
        => await _context.S_Servicios.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.S_Servicios> Handle(CreateS_ServicioCommand r, CancellationToken ct)
    {
        _context.S_Servicios.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateS_ServicioCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.S_ServicioID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteS_ServicioCommand r, CancellationToken ct)
    {
        var item = await _context.S_Servicios.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.S_Servicios.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
