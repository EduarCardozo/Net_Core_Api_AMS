using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PartesItemsOrdenesReparacion;

public record GetAllPartesItemsOrdenesReparacionQuery : IRequest<IEnumerable<Ent.PartesItemsOrdenesReparacion>>;
public record GetParteItemOrdenReparacionByIdQuery(int Id) : IRequest<Ent.PartesItemsOrdenesReparacion?>;
public record CreateParteItemOrdenReparacionCommand(Ent.PartesItemsOrdenesReparacion Item) : IRequest<Ent.PartesItemsOrdenesReparacion>;
public record UpdateParteItemOrdenReparacionCommand(int Id, Ent.PartesItemsOrdenesReparacion Item) : IRequest<bool>;
public record DeleteParteItemOrdenReparacionCommand(int Id) : IRequest<bool>;

public class PartesItemsOrdenesReparacionHandlers :
    IRequestHandler<GetAllPartesItemsOrdenesReparacionQuery, IEnumerable<Ent.PartesItemsOrdenesReparacion>>,
    IRequestHandler<GetParteItemOrdenReparacionByIdQuery, Ent.PartesItemsOrdenesReparacion?>,
    IRequestHandler<CreateParteItemOrdenReparacionCommand, Ent.PartesItemsOrdenesReparacion>,
    IRequestHandler<UpdateParteItemOrdenReparacionCommand, bool>,
    IRequestHandler<DeleteParteItemOrdenReparacionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PartesItemsOrdenesReparacionHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PartesItemsOrdenesReparacion>> Handle(GetAllPartesItemsOrdenesReparacionQuery r, CancellationToken ct)
        => await _context.PartesItemsOrdenesReparacion.ToListAsync(ct);

    public async Task<Ent.PartesItemsOrdenesReparacion?> Handle(GetParteItemOrdenReparacionByIdQuery r, CancellationToken ct)
        => await _context.PartesItemsOrdenesReparacion.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PartesItemsOrdenesReparacion> Handle(CreateParteItemOrdenReparacionCommand r, CancellationToken ct)
    {
        _context.PartesItemsOrdenesReparacion.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateParteItemOrdenReparacionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ParteItemOrdenReparacionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteParteItemOrdenReparacionCommand r, CancellationToken ct)
    {
        var item = await _context.PartesItemsOrdenesReparacion.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PartesItemsOrdenesReparacion.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
