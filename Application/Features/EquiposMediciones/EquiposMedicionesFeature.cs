using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.EquiposMediciones;

public record GetAllEquiposMedicionesQuery : IRequest<IEnumerable<Ent.EquiposMediciones>>;
public record GetEquipoMedicionByIdQuery(int Id) : IRequest<Ent.EquiposMediciones?>;
public record CreateEquipoMedicionCommand(Ent.EquiposMediciones Item) : IRequest<Ent.EquiposMediciones>;
public record UpdateEquipoMedicionCommand(int Id, Ent.EquiposMediciones Item) : IRequest<bool>;
public record DeleteEquipoMedicionCommand(int Id) : IRequest<bool>;

public class EquiposMedicionesHandlers :
    IRequestHandler<GetAllEquiposMedicionesQuery, IEnumerable<Ent.EquiposMediciones>>,
    IRequestHandler<GetEquipoMedicionByIdQuery, Ent.EquiposMediciones?>,
    IRequestHandler<CreateEquipoMedicionCommand, Ent.EquiposMediciones>,
    IRequestHandler<UpdateEquipoMedicionCommand, bool>,
    IRequestHandler<DeleteEquipoMedicionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public EquiposMedicionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.EquiposMediciones>> Handle(GetAllEquiposMedicionesQuery r, CancellationToken ct)
        => await _context.EquiposMediciones.ToListAsync(ct);

    public async Task<Ent.EquiposMediciones?> Handle(GetEquipoMedicionByIdQuery r, CancellationToken ct)
        => await _context.EquiposMediciones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.EquiposMediciones> Handle(CreateEquipoMedicionCommand r, CancellationToken ct)
    {
        _context.EquiposMediciones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateEquipoMedicionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.EquipoMedicionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteEquipoMedicionCommand r, CancellationToken ct)
    {
        var item = await _context.EquiposMediciones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.EquiposMediciones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
