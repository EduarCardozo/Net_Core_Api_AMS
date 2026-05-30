using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.PartesItemsRemitos;

public record GetAllPartesItemsRemitosQuery : IRequest<IEnumerable<Ent.PartesItemsRemitos>>;
public record CreateParteItemRemitoCommand(Ent.PartesItemsRemitos Item) : IRequest<Ent.PartesItemsRemitos>;

public class PartesItemsRemitosHandlers :
    IRequestHandler<GetAllPartesItemsRemitosQuery, IEnumerable<Ent.PartesItemsRemitos>>,
    IRequestHandler<CreateParteItemRemitoCommand, Ent.PartesItemsRemitos>
{
    private readonly AmsFlotDbContext _context;
    public PartesItemsRemitosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.PartesItemsRemitos>> Handle(GetAllPartesItemsRemitosQuery r, CancellationToken ct)
        => await _context.PartesItemsRemitos.ToListAsync(ct);

    public async Task<Ent.PartesItemsRemitos> Handle(CreateParteItemRemitoCommand r, CancellationToken ct)
    {
        _context.PartesItemsRemitos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
