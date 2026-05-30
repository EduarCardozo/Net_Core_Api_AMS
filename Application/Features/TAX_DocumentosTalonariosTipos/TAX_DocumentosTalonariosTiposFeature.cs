using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_DocumentosTalonariosTipos;

public record GetAllTAX_DocumentosTalonariosTiposQuery : IRequest<IEnumerable<Ent.TAX_DocumentosTalonariosTipos>>;
public record GetTAX_DocumentoTalonarioTipoByIdQuery(int Id) : IRequest<Ent.TAX_DocumentosTalonariosTipos?>;
public record CreateTAX_DocumentoTalonarioTipoCommand(Ent.TAX_DocumentosTalonariosTipos Item) : IRequest<Ent.TAX_DocumentosTalonariosTipos>;
public record UpdateTAX_DocumentoTalonarioTipoCommand(int Id, Ent.TAX_DocumentosTalonariosTipos Item) : IRequest<bool>;
public record DeleteTAX_DocumentoTalonarioTipoCommand(int Id) : IRequest<bool>;

public class TAX_DocumentosTalonariosTiposHandlers :
    IRequestHandler<GetAllTAX_DocumentosTalonariosTiposQuery, IEnumerable<Ent.TAX_DocumentosTalonariosTipos>>,
    IRequestHandler<GetTAX_DocumentoTalonarioTipoByIdQuery, Ent.TAX_DocumentosTalonariosTipos?>,
    IRequestHandler<CreateTAX_DocumentoTalonarioTipoCommand, Ent.TAX_DocumentosTalonariosTipos>,
    IRequestHandler<UpdateTAX_DocumentoTalonarioTipoCommand, bool>,
    IRequestHandler<DeleteTAX_DocumentoTalonarioTipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_DocumentosTalonariosTiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_DocumentosTalonariosTipos>> Handle(GetAllTAX_DocumentosTalonariosTiposQuery r, CancellationToken ct)
        => await _context.TAX_DocumentosTalonariosTipos.ToListAsync(ct);

    public async Task<Ent.TAX_DocumentosTalonariosTipos?> Handle(GetTAX_DocumentoTalonarioTipoByIdQuery r, CancellationToken ct)
        => await _context.TAX_DocumentosTalonariosTipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_DocumentosTalonariosTipos> Handle(CreateTAX_DocumentoTalonarioTipoCommand r, CancellationToken ct)
    {
        _context.TAX_DocumentosTalonariosTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_DocumentoTalonarioTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.DocumentoTalonarioTipoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_DocumentoTalonarioTipoCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_DocumentosTalonariosTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_DocumentosTalonariosTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
