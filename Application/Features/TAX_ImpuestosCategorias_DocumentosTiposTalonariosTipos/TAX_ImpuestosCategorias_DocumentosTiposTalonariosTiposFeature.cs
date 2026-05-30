using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos;

public record GetAllTAX_ImpuestosCategoriasDocumentosTiposTalonariosTiposQuery : IRequest<IEnumerable<Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos>>;
public record GetTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoByIdQuery(int Id) : IRequest<Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos?>;
public record CreateTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand(Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos Item) : IRequest<Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos>;
public record UpdateTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand(int Id, Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos Item) : IRequest<bool>;
public record DeleteTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand(int Id) : IRequest<bool>;

public class TAX_ImpuestosCategoriasDocumentosTiposTalonariosTiposHandlers :
    IRequestHandler<GetAllTAX_ImpuestosCategoriasDocumentosTiposTalonariosTiposQuery, IEnumerable<Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos>>,
    IRequestHandler<GetTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoByIdQuery, Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos?>,
    IRequestHandler<CreateTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand, Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos>,
    IRequestHandler<UpdateTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand, bool>,
    IRequestHandler<DeleteTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_ImpuestosCategoriasDocumentosTiposTalonariosTiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos>> Handle(GetAllTAX_ImpuestosCategoriasDocumentosTiposTalonariosTiposQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos.ToListAsync(ct);

    public async Task<Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos?> Handle(GetTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoByIdQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos> Handle(CreateTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand r, CancellationToken ct)
    {
        _context.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ImpuestoCategoriaDocumentoTipoTalonarioTipoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_ImpuestoCategoriaDocumentoTipoTalonarioTipoCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
