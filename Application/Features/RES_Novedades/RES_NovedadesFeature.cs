using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_Novedades;

public record GetAllRES_NovedadesQuery : IRequest<IEnumerable<Ent.RES_Novedades>>;
public record GetRES_NovedadByIdQuery(int Id) : IRequest<Ent.RES_Novedades?>;
public record CreateRES_NovedadCommand(Ent.RES_Novedades Item) : IRequest<Ent.RES_Novedades>;
public record UpdateRES_NovedadCommand(int Id, Ent.RES_Novedades Item) : IRequest<bool>;
public record DeleteRES_NovedadCommand(int Id) : IRequest<bool>;

public class RES_NovedadesHandlers :
    IRequestHandler<GetAllRES_NovedadesQuery, IEnumerable<Ent.RES_Novedades>>,
    IRequestHandler<GetRES_NovedadByIdQuery, Ent.RES_Novedades?>,
    IRequestHandler<CreateRES_NovedadCommand, Ent.RES_Novedades>,
    IRequestHandler<UpdateRES_NovedadCommand, bool>,
    IRequestHandler<DeleteRES_NovedadCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_NovedadesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_Novedades>> Handle(GetAllRES_NovedadesQuery r, CancellationToken ct)
        => await _context.RES_Novedades.ToListAsync(ct);

    public async Task<Ent.RES_Novedades?> Handle(GetRES_NovedadByIdQuery r, CancellationToken ct)
        => await _context.RES_Novedades.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_Novedades> Handle(CreateRES_NovedadCommand r, CancellationToken ct)
    {
        _context.RES_Novedades.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_NovedadCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.NovedadID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_NovedadCommand r, CancellationToken ct)
    {
        var item = await _context.RES_Novedades.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_Novedades.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
