using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TareasItems;

public record GetAllTareasItemsQuery : IRequest<IEnumerable<Ent.TareasItems>>;
public record CreateTareaItemCommand(Ent.TareasItems Item) : IRequest<Ent.TareasItems>;

public class TareasItemsHandlers :
    IRequestHandler<GetAllTareasItemsQuery, IEnumerable<Ent.TareasItems>>,
    IRequestHandler<CreateTareaItemCommand, Ent.TareasItems>
{
    private readonly AmsFlotDbContext _context;
    public TareasItemsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TareasItems>> Handle(GetAllTareasItemsQuery r, CancellationToken ct)
        => await _context.TareasItems.ToListAsync(ct);

    public async Task<Ent.TareasItems> Handle(CreateTareaItemCommand r, CancellationToken ct)
    {
        _context.TareasItems.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
