using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RevisionesEstados;

public record GetAllRevisionesEstadosQuery : IRequest<IEnumerable<Ent.RevisionesEstados>>;
public record CreateRevisionEstadoCommand(Ent.RevisionesEstados Item) : IRequest<Ent.RevisionesEstados>;

public class RevisionesEstadosHandlers :
    IRequestHandler<GetAllRevisionesEstadosQuery, IEnumerable<Ent.RevisionesEstados>>,
    IRequestHandler<CreateRevisionEstadoCommand, Ent.RevisionesEstados>
{
    private readonly AmsFlotDbContext _context;
    public RevisionesEstadosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RevisionesEstados>> Handle(GetAllRevisionesEstadosQuery r, CancellationToken ct)
        => await _context.RevisionesEstados.ToListAsync(ct);

    public async Task<Ent.RevisionesEstados> Handle(CreateRevisionEstadoCommand r, CancellationToken ct)
    {
        _context.RevisionesEstados.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }
}
