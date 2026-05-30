using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_NovedadesReemplazos;

public record GetAllRES_NovedadesReemplazosQuery : IRequest<IEnumerable<Ent.RES_NovedadesReemplazos>>;
public record GetRES_NovedadReemplazoByIdQuery(int Id) : IRequest<Ent.RES_NovedadesReemplazos?>;
public record CreateRES_NovedadReemplazoCommand(Ent.RES_NovedadesReemplazos Item) : IRequest<Ent.RES_NovedadesReemplazos>;
public record UpdateRES_NovedadReemplazoCommand(int Id, Ent.RES_NovedadesReemplazos Item) : IRequest<bool>;
public record DeleteRES_NovedadReemplazoCommand(int Id) : IRequest<bool>;

public class RES_NovedadesReemplazosHandlers :
    IRequestHandler<GetAllRES_NovedadesReemplazosQuery, IEnumerable<Ent.RES_NovedadesReemplazos>>,
    IRequestHandler<GetRES_NovedadReemplazoByIdQuery, Ent.RES_NovedadesReemplazos?>,
    IRequestHandler<CreateRES_NovedadReemplazoCommand, Ent.RES_NovedadesReemplazos>,
    IRequestHandler<UpdateRES_NovedadReemplazoCommand, bool>,
    IRequestHandler<DeleteRES_NovedadReemplazoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_NovedadesReemplazosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_NovedadesReemplazos>> Handle(GetAllRES_NovedadesReemplazosQuery r, CancellationToken ct)
        => await _context.RES_NovedadesReemplazos.ToListAsync(ct);

    public async Task<Ent.RES_NovedadesReemplazos?> Handle(GetRES_NovedadReemplazoByIdQuery r, CancellationToken ct)
        => await _context.RES_NovedadesReemplazos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_NovedadesReemplazos> Handle(CreateRES_NovedadReemplazoCommand r, CancellationToken ct)
    {
        _context.RES_NovedadesReemplazos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_NovedadReemplazoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.NovedadReemplazoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_NovedadReemplazoCommand r, CancellationToken ct)
    {
        var item = await _context.RES_NovedadesReemplazos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_NovedadesReemplazos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
