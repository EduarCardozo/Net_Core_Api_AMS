using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_AdministradoresTributariosActividadesEconomicas;

public record GetAllTAX_AdministradoresTributariosActividadesEconomicasQuery : IRequest<IEnumerable<Ent.TAX_AdministradoresTributariosActividadesEconomicas>>;
public record GetTAX_AdministradorTributarioActividadEconomicaByIdQuery(int Id) : IRequest<Ent.TAX_AdministradoresTributariosActividadesEconomicas?>;
public record CreateTAX_AdministradorTributarioActividadEconomicaCommand(Ent.TAX_AdministradoresTributariosActividadesEconomicas Item) : IRequest<Ent.TAX_AdministradoresTributariosActividadesEconomicas>;
public record UpdateTAX_AdministradorTributarioActividadEconomicaCommand(int Id, Ent.TAX_AdministradoresTributariosActividadesEconomicas Item) : IRequest<bool>;
public record DeleteTAX_AdministradorTributarioActividadEconomicaCommand(int Id) : IRequest<bool>;

public class TAX_AdministradoresTributariosActividadesEconomicasHandlers :
    IRequestHandler<GetAllTAX_AdministradoresTributariosActividadesEconomicasQuery, IEnumerable<Ent.TAX_AdministradoresTributariosActividadesEconomicas>>,
    IRequestHandler<GetTAX_AdministradorTributarioActividadEconomicaByIdQuery, Ent.TAX_AdministradoresTributariosActividadesEconomicas?>,
    IRequestHandler<CreateTAX_AdministradorTributarioActividadEconomicaCommand, Ent.TAX_AdministradoresTributariosActividadesEconomicas>,
    IRequestHandler<UpdateTAX_AdministradorTributarioActividadEconomicaCommand, bool>,
    IRequestHandler<DeleteTAX_AdministradorTributarioActividadEconomicaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_AdministradoresTributariosActividadesEconomicasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_AdministradoresTributariosActividadesEconomicas>> Handle(GetAllTAX_AdministradoresTributariosActividadesEconomicasQuery r, CancellationToken ct)
        => await _context.TAX_AdministradoresTributariosActividadesEconomicas.ToListAsync(ct);

    public async Task<Ent.TAX_AdministradoresTributariosActividadesEconomicas?> Handle(GetTAX_AdministradorTributarioActividadEconomicaByIdQuery r, CancellationToken ct)
        => await _context.TAX_AdministradoresTributariosActividadesEconomicas.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_AdministradoresTributariosActividadesEconomicas> Handle(CreateTAX_AdministradorTributarioActividadEconomicaCommand r, CancellationToken ct)
    {
        _context.TAX_AdministradoresTributariosActividadesEconomicas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_AdministradorTributarioActividadEconomicaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.AdministradorTributarioActividadEconomicaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_AdministradorTributarioActividadEconomicaCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_AdministradoresTributariosActividadesEconomicas.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_AdministradoresTributariosActividadesEconomicas.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
