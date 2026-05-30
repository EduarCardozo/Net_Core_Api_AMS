using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_PersonasTipos;

public record GetAllTAX_PersonasTiposQuery : IRequest<IEnumerable<Ent.TAX_PersonasTipos>>;
public record GetTAX_PersonaTipoByIdQuery(byte Id) : IRequest<Ent.TAX_PersonasTipos?>;
public record CreateTAX_PersonaTipoCommand(Ent.TAX_PersonasTipos Item) : IRequest<Ent.TAX_PersonasTipos>;
public record UpdateTAX_PersonaTipoCommand(byte Id, Ent.TAX_PersonasTipos Item) : IRequest<bool>;
public record DeleteTAX_PersonaTipoCommand(byte Id) : IRequest<bool>;

public class TAX_PersonasTiposHandlers :
    IRequestHandler<GetAllTAX_PersonasTiposQuery, IEnumerable<Ent.TAX_PersonasTipos>>,
    IRequestHandler<GetTAX_PersonaTipoByIdQuery, Ent.TAX_PersonasTipos?>,
    IRequestHandler<CreateTAX_PersonaTipoCommand, Ent.TAX_PersonasTipos>,
    IRequestHandler<UpdateTAX_PersonaTipoCommand, bool>,
    IRequestHandler<DeleteTAX_PersonaTipoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_PersonasTiposHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_PersonasTipos>> Handle(GetAllTAX_PersonasTiposQuery r, CancellationToken ct)
        => await _context.TAX_PersonasTipos.ToListAsync(ct);

    public async Task<Ent.TAX_PersonasTipos?> Handle(GetTAX_PersonaTipoByIdQuery r, CancellationToken ct)
        => await _context.TAX_PersonasTipos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_PersonasTipos> Handle(CreateTAX_PersonaTipoCommand r, CancellationToken ct)
    {
        _context.TAX_PersonasTipos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_PersonaTipoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PersonaTipoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_PersonaTipoCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_PersonasTipos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_PersonasTipos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
