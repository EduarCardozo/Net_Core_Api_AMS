using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PlantillasItems;

public record GetAllPlantillasItemsQuery : IRequest<IEnumerable<Ent.PlantillasItems>>;
public record CreatePlantillaItemCommand(Ent.PlantillasItems Item) : IRequest<Ent.PlantillasItems>;

public class PlantillasItemsHandlers :
    IRequestHandler<GetAllPlantillasItemsQuery, IEnumerable<Ent.PlantillasItems>>,
    IRequestHandler<CreatePlantillaItemCommand, Ent.PlantillasItems>
{
    private readonly AmsFlotDbContext _context;
    public PlantillasItemsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PlantillasItems>> Handle(GetAllPlantillasItemsQuery r, CancellationToken ct)
        => await _context.PlantillasItems.ToListAsync(ct);

    public async Task<Ent.PlantillasItems> Handle(CreatePlantillaItemCommand r, CancellationToken ct)
    {
        _context.PlantillasItems.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
