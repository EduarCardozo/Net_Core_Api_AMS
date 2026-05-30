using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.CO_UnidadesMedida;

public record GetAllCO_UnidadesMedidaQuery : IRequest<IEnumerable<Ent.CO_UnidadesMedida>>;
public record GetCO_UnidadMedidaByIdQuery(byte Id) : IRequest<Ent.CO_UnidadesMedida?>;
public record CreateCO_UnidadMedidaCommand(Ent.CO_UnidadesMedida Item) : IRequest<Ent.CO_UnidadesMedida>;
public record UpdateCO_UnidadMedidaCommand(byte Id, Ent.CO_UnidadesMedida Item) : IRequest<bool>;
public record DeleteCO_UnidadMedidaCommand(byte Id) : IRequest<bool>;

public class CO_UnidadesMedidaHandlers :
    IRequestHandler<GetAllCO_UnidadesMedidaQuery, IEnumerable<Ent.CO_UnidadesMedida>>,
    IRequestHandler<GetCO_UnidadMedidaByIdQuery, Ent.CO_UnidadesMedida?>,
    IRequestHandler<CreateCO_UnidadMedidaCommand, Ent.CO_UnidadesMedida>,
    IRequestHandler<UpdateCO_UnidadMedidaCommand, bool>,
    IRequestHandler<DeleteCO_UnidadMedidaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public CO_UnidadesMedidaHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.CO_UnidadesMedida>> Handle(GetAllCO_UnidadesMedidaQuery r, CancellationToken ct)
        => await _context.CO_UnidadesMedida.ToListAsync(ct);

    public async Task<Ent.CO_UnidadesMedida?> Handle(GetCO_UnidadMedidaByIdQuery r, CancellationToken ct)
        => await _context.CO_UnidadesMedida.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.CO_UnidadesMedida> Handle(CreateCO_UnidadMedidaCommand r, CancellationToken ct)
    {
        _context.CO_UnidadesMedida.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateCO_UnidadMedidaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.UnidadMedidaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteCO_UnidadMedidaCommand r, CancellationToken ct)
    {
        var item = await _context.CO_UnidadesMedida.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.CO_UnidadesMedida.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
