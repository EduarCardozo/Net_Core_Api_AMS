using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PlantillasTipos;

public record GetAllPlantillasTiposQuery : IRequest<IEnumerable<Ent.PlantillasTipos>>;
public record GetPlantillaTipoByIdQuery(int Id) : IRequest<Ent.PlantillasTipos?>;
public record CreatePlantillaTipoCommand(Ent.PlantillasTipos Item) : IRequest<Ent.PlantillasTipos>;
public record UpdatePlantillaTipoCommand(int Id, Ent.PlantillasTipos Item) : IRequest<bool>;
public record DeletePlantillaTipoCommand(int Id) : IRequest<bool>;

public class PlantillasTiposHandlers :
    IRequestHandler<GetAllPlantillasTiposQuery, IEnumerable<Ent.PlantillasTipos>>,
    IRequestHandler<GetPlantillaTipoByIdQuery, Ent.PlantillasTipos?>,
    IRequestHandler<CreatePlantillaTipoCommand, Ent.PlantillasTipos>,
    IRequestHandler<UpdatePlantillaTipoCommand, bool>,
    IRequestHandler<DeletePlantillaTipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PlantillasTiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PlantillasTipos>> Handle(GetAllPlantillasTiposQuery r, CancellationToken ct)
        => await _context.PlantillasTipos.ToListAsync(ct);

    public async Task<Ent.PlantillasTipos?> Handle(GetPlantillaTipoByIdQuery r, CancellationToken ct)
        => await _context.PlantillasTipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.PlantillasTipos> Handle(CreatePlantillaTipoCommand r, CancellationToken ct)
    {
        _context.PlantillasTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdatePlantillaTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PlantillaTipoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeletePlantillaTipoCommand r, CancellationToken ct)
    {
        var item = await _context.PlantillasTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.PlantillasTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
