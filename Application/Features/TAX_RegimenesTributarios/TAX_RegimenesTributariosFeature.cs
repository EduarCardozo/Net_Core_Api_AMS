using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_RegimenesTributarios;

public record GetAllTAX_RegimenesTributariosQuery : IRequest<IEnumerable<Ent.TAX_RegimenesTributarios>>;
public record GetTAX_RegimenTributarioByIdQuery(int Id) : IRequest<Ent.TAX_RegimenesTributarios?>;
public record CreateTAX_RegimenTributarioCommand(Ent.TAX_RegimenesTributarios Item) : IRequest<Ent.TAX_RegimenesTributarios>;
public record UpdateTAX_RegimenTributarioCommand(int Id, Ent.TAX_RegimenesTributarios Item) : IRequest<bool>;
public record DeleteTAX_RegimenTributarioCommand(int Id) : IRequest<bool>;

public class TAX_RegimenesTributariosHandlers :
    IRequestHandler<GetAllTAX_RegimenesTributariosQuery, IEnumerable<Ent.TAX_RegimenesTributarios>>,
    IRequestHandler<GetTAX_RegimenTributarioByIdQuery, Ent.TAX_RegimenesTributarios?>,
    IRequestHandler<CreateTAX_RegimenTributarioCommand, Ent.TAX_RegimenesTributarios>,
    IRequestHandler<UpdateTAX_RegimenTributarioCommand, bool>,
    IRequestHandler<DeleteTAX_RegimenTributarioCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_RegimenesTributariosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_RegimenesTributarios>> Handle(GetAllTAX_RegimenesTributariosQuery r, CancellationToken ct)
        => await _context.TAX_RegimenesTributarios.ToListAsync(ct);

    public async Task<Ent.TAX_RegimenesTributarios?> Handle(GetTAX_RegimenTributarioByIdQuery r, CancellationToken ct)
        => await _context.TAX_RegimenesTributarios.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_RegimenesTributarios> Handle(CreateTAX_RegimenTributarioCommand r, CancellationToken ct)
    {
        _context.TAX_RegimenesTributarios.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_RegimenTributarioCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.RegimenTributarioID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_RegimenTributarioCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_RegimenesTributarios.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_RegimenesTributarios.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
