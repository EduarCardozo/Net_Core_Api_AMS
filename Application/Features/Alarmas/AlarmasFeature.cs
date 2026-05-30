using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Alarmas;

public record GetAllAlarmasQuery : IRequest<IEnumerable<Ent.Alarmas>>;
public record GetAlarmaByIdQuery(int Id) : IRequest<Ent.Alarmas?>;
public record CreateAlarmaCommand(Ent.Alarmas Item) : IRequest<Ent.Alarmas>;
public record UpdateAlarmaCommand(int Id, Ent.Alarmas Item) : IRequest<bool>;
public record DeleteAlarmaCommand(int Id) : IRequest<bool>;

public class AlarmasHandlers :
    IRequestHandler<GetAllAlarmasQuery, IEnumerable<Ent.Alarmas>>,
    IRequestHandler<GetAlarmaByIdQuery, Ent.Alarmas?>,
    IRequestHandler<CreateAlarmaCommand, Ent.Alarmas>,
    IRequestHandler<UpdateAlarmaCommand, bool>,
    IRequestHandler<DeleteAlarmaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public AlarmasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Alarmas>> Handle(GetAllAlarmasQuery r, CancellationToken ct)
        => await _context.Alarmas.ToListAsync(ct);

    public async Task<Ent.Alarmas?> Handle(GetAlarmaByIdQuery r, CancellationToken ct)
        => await _context.Alarmas.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Alarmas> Handle(CreateAlarmaCommand r, CancellationToken ct)
    {
        _context.Alarmas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateAlarmaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.AlarmaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteAlarmaCommand r, CancellationToken ct)
    {
        var item = await _context.Alarmas.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Alarmas.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
