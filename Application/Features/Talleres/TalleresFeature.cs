using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Talleres;

public record GetAllTalleresQuery : IRequest<IEnumerable<Ent.Talleres>>;
public record GetTallerByIdQuery(int Id) : IRequest<Ent.Talleres?>;
public record CreateTallerCommand(Ent.Talleres Item) : IRequest<Ent.Talleres>;
public record UpdateTallerCommand(int Id, Ent.Talleres Item) : IRequest<bool>;
public record DeleteTallerCommand(int Id) : IRequest<bool>;

public class TalleresHandlers :
    IRequestHandler<GetAllTalleresQuery, IEnumerable<Ent.Talleres>>,
    IRequestHandler<GetTallerByIdQuery, Ent.Talleres?>,
    IRequestHandler<CreateTallerCommand, Ent.Talleres>,
    IRequestHandler<UpdateTallerCommand, bool>,
    IRequestHandler<DeleteTallerCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TalleresHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Talleres>> Handle(GetAllTalleresQuery r, CancellationToken ct)
        => await _context.Talleres.ToListAsync(ct);

    public async Task<Ent.Talleres?> Handle(GetTallerByIdQuery r, CancellationToken ct)
        => await _context.Talleres.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Talleres> Handle(CreateTallerCommand r, CancellationToken ct)
    {
        _context.Talleres.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTallerCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.TallerID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTallerCommand r, CancellationToken ct)
    {
        var item = await _context.Talleres.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Talleres.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
