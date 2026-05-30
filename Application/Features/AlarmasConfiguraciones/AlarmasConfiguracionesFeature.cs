using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.AlarmasConfiguraciones;

public record GetAllAlarmasConfiguracionesQuery : IRequest<IEnumerable<Ent.AlarmasConfiguraciones>>;
public record GetAlarmaConfiguracionByIdQuery(int Id) : IRequest<Ent.AlarmasConfiguraciones?>;
public record CreateAlarmaConfiguracionCommand(Ent.AlarmasConfiguraciones Item) : IRequest<Ent.AlarmasConfiguraciones>;
public record UpdateAlarmaConfiguracionCommand(int Id, Ent.AlarmasConfiguraciones Item) : IRequest<bool>;
public record DeleteAlarmaConfiguracionCommand(int Id) : IRequest<bool>;

public class AlarmasConfiguracionesHandlers :
    IRequestHandler<GetAllAlarmasConfiguracionesQuery, IEnumerable<Ent.AlarmasConfiguraciones>>,
    IRequestHandler<GetAlarmaConfiguracionByIdQuery, Ent.AlarmasConfiguraciones?>,
    IRequestHandler<CreateAlarmaConfiguracionCommand, Ent.AlarmasConfiguraciones>,
    IRequestHandler<UpdateAlarmaConfiguracionCommand, bool>,
    IRequestHandler<DeleteAlarmaConfiguracionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public AlarmasConfiguracionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.AlarmasConfiguraciones>> Handle(GetAllAlarmasConfiguracionesQuery r, CancellationToken ct)
        => await _context.AlarmasConfiguraciones.ToListAsync(ct);

    public async Task<Ent.AlarmasConfiguraciones?> Handle(GetAlarmaConfiguracionByIdQuery r, CancellationToken ct)
        => await _context.AlarmasConfiguraciones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.AlarmasConfiguraciones> Handle(CreateAlarmaConfiguracionCommand r, CancellationToken ct)
    {
        _context.AlarmasConfiguraciones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateAlarmaConfiguracionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.AlarmaConfiguracionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteAlarmaConfiguracionCommand r, CancellationToken ct)
    {
        var item = await _context.AlarmasConfiguraciones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.AlarmasConfiguraciones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
