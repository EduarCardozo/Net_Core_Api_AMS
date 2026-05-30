using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Rubros;

public record GetAllRubrosQuery : IRequest<IEnumerable<Ent.Rubros>>;
public record GetRubroByIdQuery(int Id) : IRequest<Ent.Rubros?>;
public record CreateRubroCommand(Ent.Rubros Item) : IRequest<Ent.Rubros>;
public record UpdateRubroCommand(int Id, Ent.Rubros Item) : IRequest<bool>;
public record DeleteRubroCommand(int Id) : IRequest<bool>;

public class RubrosHandlers :
    IRequestHandler<GetAllRubrosQuery, IEnumerable<Ent.Rubros>>,
    IRequestHandler<GetRubroByIdQuery, Ent.Rubros?>,
    IRequestHandler<CreateRubroCommand, Ent.Rubros>,
    IRequestHandler<UpdateRubroCommand, bool>,
    IRequestHandler<DeleteRubroCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RubrosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Rubros>> Handle(GetAllRubrosQuery r, CancellationToken ct)
        => await _context.Rubros.ToListAsync(ct);

    public async Task<Ent.Rubros?> Handle(GetRubroByIdQuery r, CancellationToken ct)
        => await _context.Rubros.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Rubros> Handle(CreateRubroCommand r, CancellationToken ct)
    {
        _context.Rubros.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRubroCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.RubroID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRubroCommand r, CancellationToken ct)
    {
        var item = await _context.Rubros.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Rubros.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
