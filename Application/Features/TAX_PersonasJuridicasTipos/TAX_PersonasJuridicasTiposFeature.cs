using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_PersonasJuridicasTipos;

public record GetAllTAX_PersonasJuridicasTiposQuery : IRequest<IEnumerable<Ent.TAX_PersonasJuridicasTipos>>;
public record GetTAX_PersonaJuridicaTipoByIdQuery(byte Id) : IRequest<Ent.TAX_PersonasJuridicasTipos?>;
public record CreateTAX_PersonaJuridicaTipoCommand(Ent.TAX_PersonasJuridicasTipos Item) : IRequest<Ent.TAX_PersonasJuridicasTipos>;
public record UpdateTAX_PersonaJuridicaTipoCommand(byte Id, Ent.TAX_PersonasJuridicasTipos Item) : IRequest<bool>;
public record DeleteTAX_PersonaJuridicaTipoCommand(byte Id) : IRequest<bool>;

public class TAX_PersonasJuridicasTiposHandlers :
    IRequestHandler<GetAllTAX_PersonasJuridicasTiposQuery, IEnumerable<Ent.TAX_PersonasJuridicasTipos>>,
    IRequestHandler<GetTAX_PersonaJuridicaTipoByIdQuery, Ent.TAX_PersonasJuridicasTipos?>,
    IRequestHandler<CreateTAX_PersonaJuridicaTipoCommand, Ent.TAX_PersonasJuridicasTipos>,
    IRequestHandler<UpdateTAX_PersonaJuridicaTipoCommand, bool>,
    IRequestHandler<DeleteTAX_PersonaJuridicaTipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_PersonasJuridicasTiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_PersonasJuridicasTipos>> Handle(GetAllTAX_PersonasJuridicasTiposQuery r, CancellationToken ct)
        => await _context.TAX_PersonasJuridicasTipos.ToListAsync(ct);

    public async Task<Ent.TAX_PersonasJuridicasTipos?> Handle(GetTAX_PersonaJuridicaTipoByIdQuery r, CancellationToken ct)
        => await _context.TAX_PersonasJuridicasTipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_PersonasJuridicasTipos> Handle(CreateTAX_PersonaJuridicaTipoCommand r, CancellationToken ct)
    {
        _context.TAX_PersonasJuridicasTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_PersonaJuridicaTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PersonaJuridicaTipoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_PersonaJuridicaTipoCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_PersonasJuridicasTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_PersonasJuridicasTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
