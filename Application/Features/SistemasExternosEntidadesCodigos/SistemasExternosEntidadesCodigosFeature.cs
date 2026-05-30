using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.SistemasExternosEntidadesCodigos;

public record GetAllSistemasExternosEntidadesCodigosQuery : IRequest<IEnumerable<Ent.SistemasExternosEntidadesCodigos>>;
public record GetSistemaExternoEntidadCodigoByIdQuery(int Id) : IRequest<Ent.SistemasExternosEntidadesCodigos?>;
public record CreateSistemaExternoEntidadCodigoCommand(Ent.SistemasExternosEntidadesCodigos Item) : IRequest<Ent.SistemasExternosEntidadesCodigos>;
public record UpdateSistemaExternoEntidadCodigoCommand(int Id, Ent.SistemasExternosEntidadesCodigos Item) : IRequest<bool>;
public record DeleteSistemaExternoEntidadCodigoCommand(int Id) : IRequest<bool>;

public class SistemasExternosEntidadesCodigosHandlers :
    IRequestHandler<GetAllSistemasExternosEntidadesCodigosQuery, IEnumerable<Ent.SistemasExternosEntidadesCodigos>>,
    IRequestHandler<GetSistemaExternoEntidadCodigoByIdQuery, Ent.SistemasExternosEntidadesCodigos?>,
    IRequestHandler<CreateSistemaExternoEntidadCodigoCommand, Ent.SistemasExternosEntidadesCodigos>,
    IRequestHandler<UpdateSistemaExternoEntidadCodigoCommand, bool>,
    IRequestHandler<DeleteSistemaExternoEntidadCodigoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public SistemasExternosEntidadesCodigosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.SistemasExternosEntidadesCodigos>> Handle(GetAllSistemasExternosEntidadesCodigosQuery r, CancellationToken ct)
        => await _context.SistemasExternosEntidadesCodigos.ToListAsync(ct);

    public async Task<Ent.SistemasExternosEntidadesCodigos?> Handle(GetSistemaExternoEntidadCodigoByIdQuery r, CancellationToken ct)
        => await _context.SistemasExternosEntidadesCodigos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.SistemasExternosEntidadesCodigos> Handle(CreateSistemaExternoEntidadCodigoCommand r, CancellationToken ct)
    {
        _context.SistemasExternosEntidadesCodigos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateSistemaExternoEntidadCodigoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.SistemaExternoEntidadCodigoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteSistemaExternoEntidadCodigoCommand r, CancellationToken ct)
    {
        var item = await _context.SistemasExternosEntidadesCodigos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.SistemasExternosEntidadesCodigos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
