using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Perfiles;

public record GetAllPerfilesQuery : IRequest<IEnumerable<Ent.Perfiles>>;
public record GetPerfilByIdQuery(int Id) : IRequest<Ent.Perfiles?>;
public record CreatePerfilCommand(Ent.Perfiles Item) : IRequest<Ent.Perfiles>;
public record UpdatePerfilCommand(int Id, Ent.Perfiles Item) : IRequest<bool>;
public record DeletePerfilCommand(int Id) : IRequest<bool>;

public class PerfilesHandlers :
    IRequestHandler<GetAllPerfilesQuery, IEnumerable<Ent.Perfiles>>,
    IRequestHandler<GetPerfilByIdQuery, Ent.Perfiles?>,
    IRequestHandler<CreatePerfilCommand, Ent.Perfiles>,
    IRequestHandler<UpdatePerfilCommand, bool>,
    IRequestHandler<DeletePerfilCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PerfilesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Perfiles>> Handle(GetAllPerfilesQuery r, CancellationToken ct)
        => await _context.Perfiles.ToListAsync(ct);

    public async Task<Ent.Perfiles?> Handle(GetPerfilByIdQuery r, CancellationToken ct)
        => await _context.Perfiles.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Perfiles> Handle(CreatePerfilCommand r, CancellationToken ct)
    {
        _context.Perfiles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdatePerfilCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PerfilID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeletePerfilCommand r, CancellationToken ct)
    {
        var item = await _context.Perfiles.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Perfiles.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
