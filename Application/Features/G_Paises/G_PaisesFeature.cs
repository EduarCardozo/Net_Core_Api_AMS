using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.G_Paises;

public record GetAllG_PaisesQuery : IRequest<IEnumerable<Ent.G_Paises>>;
public record GetG_PaisByIdQuery(short Id) : IRequest<Ent.G_Paises?>;
public record CreateG_PaisCommand(Ent.G_Paises Item) : IRequest<Ent.G_Paises>;
public record UpdateG_PaisCommand(short Id, Ent.G_Paises Item) : IRequest<bool>;
public record DeleteG_PaisCommand(short Id) : IRequest<bool>;

public class G_PaisesHandlers :
    IRequestHandler<GetAllG_PaisesQuery, IEnumerable<Ent.G_Paises>>,
    IRequestHandler<GetG_PaisByIdQuery, Ent.G_Paises?>,
    IRequestHandler<CreateG_PaisCommand, Ent.G_Paises>,
    IRequestHandler<UpdateG_PaisCommand, bool>,
    IRequestHandler<DeleteG_PaisCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public G_PaisesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.G_Paises>> Handle(GetAllG_PaisesQuery r, CancellationToken ct)
        => await _context.G_Paises.ToListAsync(ct);

    public async Task<Ent.G_Paises?> Handle(GetG_PaisByIdQuery r, CancellationToken ct)
        => await _context.G_Paises.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.G_Paises> Handle(CreateG_PaisCommand r, CancellationToken ct)
    {
        _context.G_Paises.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateG_PaisCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.PaisID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteG_PaisCommand r, CancellationToken ct)
    {
        var item = await _context.G_Paises.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.G_Paises.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
