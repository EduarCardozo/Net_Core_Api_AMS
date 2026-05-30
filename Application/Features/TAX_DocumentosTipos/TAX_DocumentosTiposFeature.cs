using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_DocumentosTipos;

public record GetAllTAX_DocumentosTiposQuery : IRequest<IEnumerable<Ent.TAX_DocumentosTipos>>;
public record GetTAX_DocumentoTipoByIdQuery(int Id) : IRequest<Ent.TAX_DocumentosTipos?>;
public record CreateTAX_DocumentoTipoCommand(Ent.TAX_DocumentosTipos Item) : IRequest<Ent.TAX_DocumentosTipos>;
public record UpdateTAX_DocumentoTipoCommand(int Id, Ent.TAX_DocumentosTipos Item) : IRequest<bool>;
public record DeleteTAX_DocumentoTipoCommand(int Id) : IRequest<bool>;

public class TAX_DocumentosTiposHandlers :
    IRequestHandler<GetAllTAX_DocumentosTiposQuery, IEnumerable<Ent.TAX_DocumentosTipos>>,
    IRequestHandler<GetTAX_DocumentoTipoByIdQuery, Ent.TAX_DocumentosTipos?>,
    IRequestHandler<CreateTAX_DocumentoTipoCommand, Ent.TAX_DocumentosTipos>,
    IRequestHandler<UpdateTAX_DocumentoTipoCommand, bool>,
    IRequestHandler<DeleteTAX_DocumentoTipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_DocumentosTiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_DocumentosTipos>> Handle(GetAllTAX_DocumentosTiposQuery r, CancellationToken ct)
        => await _context.TAX_DocumentosTipos.ToListAsync(ct);

    public async Task<Ent.TAX_DocumentosTipos?> Handle(GetTAX_DocumentoTipoByIdQuery r, CancellationToken ct)
        => await _context.TAX_DocumentosTipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_DocumentosTipos> Handle(CreateTAX_DocumentoTipoCommand r, CancellationToken ct)
    {
        _context.TAX_DocumentosTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_DocumentoTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.DocumentoTipoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_DocumentoTipoCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_DocumentosTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_DocumentosTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
