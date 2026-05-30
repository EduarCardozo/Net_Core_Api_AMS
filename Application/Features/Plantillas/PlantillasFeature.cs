using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Plantillas;

public record GetAllPlantillasQuery : IRequest<IEnumerable<Ent.Plantillas>>;
public record GetPlantillaByIdQuery(int Id) : IRequest<Ent.Plantillas?>;
public record CreatePlantillaCommand(Ent.Plantillas Item) : IRequest<Ent.Plantillas>;
public record UpdatePlantillaCommand(int Id, Ent.Plantillas Item) : IRequest<bool>;
public record DeletePlantillaCommand(int Id) : IRequest<bool>;

public class PlantillasHandlers :
    IRequestHandler<GetAllPlantillasQuery, IEnumerable<Ent.Plantillas>>,
    IRequestHandler<GetPlantillaByIdQuery, Ent.Plantillas?>,
    IRequestHandler<CreatePlantillaCommand, Ent.Plantillas>,
    IRequestHandler<UpdatePlantillaCommand, bool>,
    IRequestHandler<DeletePlantillaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PlantillasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Plantillas>> Handle(GetAllPlantillasQuery r, CancellationToken ct)
        => await _context.Plantillas.ToListAsync(ct);

    public async Task<Ent.Plantillas?> Handle(GetPlantillaByIdQuery r, CancellationToken ct)
        => await _context.Plantillas.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Plantillas> Handle(CreatePlantillaCommand r, CancellationToken ct)
    {
        _context.Plantillas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdatePlantillaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PlantillaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeletePlantillaCommand r, CancellationToken ct)
    {
        var item = await _context.Plantillas.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Plantillas.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
