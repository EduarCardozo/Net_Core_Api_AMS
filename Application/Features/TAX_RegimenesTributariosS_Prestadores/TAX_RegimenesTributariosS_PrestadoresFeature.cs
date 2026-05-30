using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_RegimenesTributariosS_Prestadores;

public record GetAllTAX_RegimenesTributariosS_PrestadoresQuery : IRequest<IEnumerable<Ent.TAX_RegimenesTributariosS_Prestadores>>;
public record GetTAX_RegimenTributarioS_PrestadorByIdQuery(int Id) : IRequest<Ent.TAX_RegimenesTributariosS_Prestadores?>;
public record CreateTAX_RegimenTributarioS_PrestadorCommand(Ent.TAX_RegimenesTributariosS_Prestadores Item) : IRequest<Ent.TAX_RegimenesTributariosS_Prestadores>;
public record UpdateTAX_RegimenTributarioS_PrestadorCommand(int Id, Ent.TAX_RegimenesTributariosS_Prestadores Item) : IRequest<bool>;
public record DeleteTAX_RegimenTributarioS_PrestadorCommand(int Id) : IRequest<bool>;

public class TAX_RegimenesTributariosS_PrestadoresHandlers :
    IRequestHandler<GetAllTAX_RegimenesTributariosS_PrestadoresQuery, IEnumerable<Ent.TAX_RegimenesTributariosS_Prestadores>>,
    IRequestHandler<GetTAX_RegimenTributarioS_PrestadorByIdQuery, Ent.TAX_RegimenesTributariosS_Prestadores?>,
    IRequestHandler<CreateTAX_RegimenTributarioS_PrestadorCommand, Ent.TAX_RegimenesTributariosS_Prestadores>,
    IRequestHandler<UpdateTAX_RegimenTributarioS_PrestadorCommand, bool>,
    IRequestHandler<DeleteTAX_RegimenTributarioS_PrestadorCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_RegimenesTributariosS_PrestadoresHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_RegimenesTributariosS_Prestadores>> Handle(GetAllTAX_RegimenesTributariosS_PrestadoresQuery r, CancellationToken ct)
        => await _context.TAX_RegimenesTributariosS_Prestadores.ToListAsync(ct);

    public async Task<Ent.TAX_RegimenesTributariosS_Prestadores?> Handle(GetTAX_RegimenTributarioS_PrestadorByIdQuery r, CancellationToken ct)
        => await _context.TAX_RegimenesTributariosS_Prestadores.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_RegimenesTributariosS_Prestadores> Handle(CreateTAX_RegimenTributarioS_PrestadorCommand r, CancellationToken ct)
    {
        _context.TAX_RegimenesTributariosS_Prestadores.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_RegimenTributarioS_PrestadorCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.RegimenTributarioS_PrestadorID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_RegimenTributarioS_PrestadorCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_RegimenesTributariosS_Prestadores.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_RegimenesTributariosS_Prestadores.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
