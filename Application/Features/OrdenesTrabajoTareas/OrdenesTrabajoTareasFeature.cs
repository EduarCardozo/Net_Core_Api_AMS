using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.OrdenesTrabajoTareas;

public record GetAllOrdenesTrabajoTareasQuery : IRequest<IEnumerable<Ent.OrdenesTrabajoTareas>>;
public record CreateOrdenTrabajoTareaCommand(Ent.OrdenesTrabajoTareas Item) : IRequest<Ent.OrdenesTrabajoTareas>;

public class OrdenesTrabajoTareasHandlers :
    IRequestHandler<GetAllOrdenesTrabajoTareasQuery, IEnumerable<Ent.OrdenesTrabajoTareas>>,
    IRequestHandler<CreateOrdenTrabajoTareaCommand, Ent.OrdenesTrabajoTareas>
{
    private readonly AmsFlotDbContext _context;
    public OrdenesTrabajoTareasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.OrdenesTrabajoTareas>> Handle(GetAllOrdenesTrabajoTareasQuery r, CancellationToken ct)
        => await _context.OrdenesTrabajoTareas.ToListAsync(ct);

    public async Task<Ent.OrdenesTrabajoTareas> Handle(CreateOrdenTrabajoTareaCommand r, CancellationToken ct)
    {
        _context.OrdenesTrabajoTareas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
