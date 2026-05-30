using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.G_Localidades;

public record GetAllG_LocalidadesQuery : IRequest<IEnumerable<Ent.G_Localidades>>;
public record GetG_LocalidadByIdQuery(int Id) : IRequest<Ent.G_Localidades?>;
public record CreateG_LocalidadCommand(Ent.G_Localidades Item) : IRequest<Ent.G_Localidades>;
public record UpdateG_LocalidadCommand(int Id, Ent.G_Localidades Item) : IRequest<bool>;
public record DeleteG_LocalidadCommand(int Id) : IRequest<bool>;

public class G_LocalidadesHandlers :
    IRequestHandler<GetAllG_LocalidadesQuery, IEnumerable<Ent.G_Localidades>>,
    IRequestHandler<GetG_LocalidadByIdQuery, Ent.G_Localidades?>,
    IRequestHandler<CreateG_LocalidadCommand, Ent.G_Localidades>,
    IRequestHandler<UpdateG_LocalidadCommand, bool>,
    IRequestHandler<DeleteG_LocalidadCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public G_LocalidadesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.G_Localidades>> Handle(GetAllG_LocalidadesQuery r, CancellationToken ct)
        => await _context.G_Localidades.ToListAsync(ct);

    public async Task<Ent.G_Localidades?> Handle(GetG_LocalidadByIdQuery r, CancellationToken ct)
        => await _context.G_Localidades.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.G_Localidades> Handle(CreateG_LocalidadCommand r, CancellationToken ct)
    {
        _context.G_Localidades.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateG_LocalidadCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.LocalidadID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteG_LocalidadCommand r, CancellationToken ct)
    {
        var item = await _context.G_Localidades.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.G_Localidades.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
