using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TareasPartes;

public record GetAllTareasPartesQuery : IRequest<IEnumerable<Ent.TareasPartes>>;
public record CreateTareaParteCommand(Ent.TareasPartes Item) : IRequest<Ent.TareasPartes>;

public class TareasPartesHandlers :
    IRequestHandler<GetAllTareasPartesQuery, IEnumerable<Ent.TareasPartes>>,
    IRequestHandler<CreateTareaParteCommand, Ent.TareasPartes>
{
    private readonly AmsFlotDbContext _context;
    public TareasPartesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TareasPartes>> Handle(GetAllTareasPartesQuery r, CancellationToken ct)
        => await _context.TareasPartes.ToListAsync(ct);

    public async Task<Ent.TareasPartes> Handle(CreateTareaParteCommand r, CancellationToken ct)
    {
        _context.TareasPartes.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
