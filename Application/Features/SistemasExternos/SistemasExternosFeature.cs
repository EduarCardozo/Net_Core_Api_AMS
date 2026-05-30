using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.SistemasExternos;

public record GetAllSistemasExternosQuery : IRequest<IEnumerable<Ent.SistemasExternos>>;
public record GetSistemaExternoByIdQuery(short Id) : IRequest<Ent.SistemasExternos?>;
public record CreateSistemaExternoCommand(Ent.SistemasExternos Item) : IRequest<Ent.SistemasExternos>;
public record UpdateSistemaExternoCommand(short Id, Ent.SistemasExternos Item) : IRequest<bool>;
public record DeleteSistemaExternoCommand(short Id) : IRequest<bool>;

public class SistemasExternosHandlers :
    IRequestHandler<GetAllSistemasExternosQuery, IEnumerable<Ent.SistemasExternos>>,
    IRequestHandler<GetSistemaExternoByIdQuery, Ent.SistemasExternos?>,
    IRequestHandler<CreateSistemaExternoCommand, Ent.SistemasExternos>,
    IRequestHandler<UpdateSistemaExternoCommand, bool>,
    IRequestHandler<DeleteSistemaExternoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public SistemasExternosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.SistemasExternos>> Handle(GetAllSistemasExternosQuery r, CancellationToken ct)
        => await _context.SistemasExternos.ToListAsync(ct);

    public async Task<Ent.SistemasExternos?> Handle(GetSistemaExternoByIdQuery r, CancellationToken ct)
        => await _context.SistemasExternos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.SistemasExternos> Handle(CreateSistemaExternoCommand r, CancellationToken ct)
    {
        _context.SistemasExternos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateSistemaExternoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.SistemaExternoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteSistemaExternoCommand r, CancellationToken ct)
    {
        var item = await _context.SistemasExternos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.SistemasExternos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
