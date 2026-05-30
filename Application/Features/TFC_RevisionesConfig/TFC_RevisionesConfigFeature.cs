using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TFC_RevisionesConfig;

public record GetAllTFC_RevisionesConfigQuery : IRequest<IEnumerable<Ent.TFC_RevisionesConfig>>;
public record GetTFC_RevisionConfigByIdQuery(int Id) : IRequest<Ent.TFC_RevisionesConfig?>;
public record CreateTFC_RevisionConfigCommand(Ent.TFC_RevisionesConfig Item) : IRequest<Ent.TFC_RevisionesConfig>;
public record UpdateTFC_RevisionConfigCommand(int Id, Ent.TFC_RevisionesConfig Item) : IRequest<bool>;
public record DeleteTFC_RevisionConfigCommand(int Id) : IRequest<bool>;

public class TFC_RevisionesConfigHandlers :
    IRequestHandler<GetAllTFC_RevisionesConfigQuery, IEnumerable<Ent.TFC_RevisionesConfig>>,
    IRequestHandler<GetTFC_RevisionConfigByIdQuery, Ent.TFC_RevisionesConfig?>,
    IRequestHandler<CreateTFC_RevisionConfigCommand, Ent.TFC_RevisionesConfig>,
    IRequestHandler<UpdateTFC_RevisionConfigCommand, bool>,
    IRequestHandler<DeleteTFC_RevisionConfigCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TFC_RevisionesConfigHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TFC_RevisionesConfig>> Handle(GetAllTFC_RevisionesConfigQuery r, CancellationToken ct)
        => await _context.TFC_RevisionesConfig.ToListAsync(ct);

    public async Task<Ent.TFC_RevisionesConfig?> Handle(GetTFC_RevisionConfigByIdQuery r, CancellationToken ct)
        => await _context.TFC_RevisionesConfig.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TFC_RevisionesConfig> Handle(CreateTFC_RevisionConfigCommand r, CancellationToken ct)
    {
        _context.TFC_RevisionesConfig.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTFC_RevisionConfigCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.RevisionConfigID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTFC_RevisionConfigCommand r, CancellationToken ct)
    {
        var item = await _context.TFC_RevisionesConfig.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TFC_RevisionesConfig.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
