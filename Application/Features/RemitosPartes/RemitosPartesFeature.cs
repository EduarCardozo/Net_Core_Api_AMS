using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RemitosPartes;

public record GetAllRemitosPartesQuery : IRequest<IEnumerable<Ent.RemitosPartes>>;
public record CreateRemitoParteCommand(Ent.RemitosPartes Item) : IRequest<Ent.RemitosPartes>;

public class RemitosPartesHandlers :
    IRequestHandler<GetAllRemitosPartesQuery, IEnumerable<Ent.RemitosPartes>>,
    IRequestHandler<CreateRemitoParteCommand, Ent.RemitosPartes>
{
    private readonly AmsFlotDbContext _context;
    public RemitosPartesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RemitosPartes>> Handle(GetAllRemitosPartesQuery r, CancellationToken ct)
        => await _context.RemitosPartes.ToListAsync(ct);

    public async Task<Ent.RemitosPartes> Handle(CreateRemitoParteCommand r, CancellationToken ct)
    {
        _context.RemitosPartes.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
