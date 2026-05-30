using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.DocumentosElectronicos;

public record GetAllDocumentosElectronicosQuery : IRequest<IEnumerable<Ent.DocumentosElectronicos>>;
public record GetDocumentoElectronicoByIdQuery(int Id) : IRequest<Ent.DocumentosElectronicos?>;
public record CreateDocumentoElectronicoCommand(Ent.DocumentosElectronicos Item) : IRequest<Ent.DocumentosElectronicos>;
public record UpdateDocumentoElectronicoCommand(int Id, Ent.DocumentosElectronicos Item) : IRequest<bool>;
public record DeleteDocumentoElectronicoCommand(int Id) : IRequest<bool>;

public class DocumentosElectronicosHandlers :
    IRequestHandler<GetAllDocumentosElectronicosQuery, IEnumerable<Ent.DocumentosElectronicos>>,
    IRequestHandler<GetDocumentoElectronicoByIdQuery, Ent.DocumentosElectronicos?>,
    IRequestHandler<CreateDocumentoElectronicoCommand, Ent.DocumentosElectronicos>,
    IRequestHandler<UpdateDocumentoElectronicoCommand, bool>,
    IRequestHandler<DeleteDocumentoElectronicoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public DocumentosElectronicosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.DocumentosElectronicos>> Handle(GetAllDocumentosElectronicosQuery r, CancellationToken ct)
        => await _context.DocumentosElectronicos.ToListAsync(ct);

    public async Task<Ent.DocumentosElectronicos?> Handle(GetDocumentoElectronicoByIdQuery r, CancellationToken ct)
        => await _context.DocumentosElectronicos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.DocumentosElectronicos> Handle(CreateDocumentoElectronicoCommand r, CancellationToken ct)
    {
        _context.DocumentosElectronicos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateDocumentoElectronicoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.DocumentoElectronicoId) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteDocumentoElectronicoCommand r, CancellationToken ct)
    {
        var item = await _context.DocumentosElectronicos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.DocumentosElectronicos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
