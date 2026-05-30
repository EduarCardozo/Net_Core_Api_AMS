using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_ImpuestosRoles;

public record GetAllTAX_ImpuestosRolesQuery : IRequest<IEnumerable<Ent.TAX_ImpuestosRoles>>;
public record GetTAX_ImpuestoRolByIdQuery(byte Id) : IRequest<Ent.TAX_ImpuestosRoles?>;
public record CreateTAX_ImpuestoRolCommand(Ent.TAX_ImpuestosRoles Item) : IRequest<Ent.TAX_ImpuestosRoles>;
public record UpdateTAX_ImpuestoRolCommand(byte Id, Ent.TAX_ImpuestosRoles Item) : IRequest<bool>;
public record DeleteTAX_ImpuestoRolCommand(byte Id) : IRequest<bool>;

public class TAX_ImpuestosRolesHandlers :
    IRequestHandler<GetAllTAX_ImpuestosRolesQuery, IEnumerable<Ent.TAX_ImpuestosRoles>>,
    IRequestHandler<GetTAX_ImpuestoRolByIdQuery, Ent.TAX_ImpuestosRoles?>,
    IRequestHandler<CreateTAX_ImpuestoRolCommand, Ent.TAX_ImpuestosRoles>,
    IRequestHandler<UpdateTAX_ImpuestoRolCommand, bool>,
    IRequestHandler<DeleteTAX_ImpuestoRolCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_ImpuestosRolesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_ImpuestosRoles>> Handle(GetAllTAX_ImpuestosRolesQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosRoles.ToListAsync(ct);

    public async Task<Ent.TAX_ImpuestosRoles?> Handle(GetTAX_ImpuestoRolByIdQuery r, CancellationToken ct)
        => await _context.TAX_ImpuestosRoles.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_ImpuestosRoles> Handle(CreateTAX_ImpuestoRolCommand r, CancellationToken ct)
    {
        _context.TAX_ImpuestosRoles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_ImpuestoRolCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ImpuestoRolID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_ImpuestoRolCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_ImpuestosRoles.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_ImpuestosRoles.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
