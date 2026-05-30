using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Roles;

public record GetAllRolesQuery : IRequest<IEnumerable<Ent.Roles>>;
public record GetRolByIdQuery(short Id) : IRequest<Ent.Roles?>;
public record CreateRolCommand(Ent.Roles Item) : IRequest<Ent.Roles>;
public record UpdateRolCommand(short Id, Ent.Roles Item) : IRequest<bool>;
public record DeleteRolCommand(short Id) : IRequest<bool>;

public class RolesHandlers :
    IRequestHandler<GetAllRolesQuery, IEnumerable<Ent.Roles>>,
    IRequestHandler<GetRolByIdQuery, Ent.Roles?>,
    IRequestHandler<CreateRolCommand, Ent.Roles>,
    IRequestHandler<UpdateRolCommand, bool>,
    IRequestHandler<DeleteRolCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RolesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Roles>> Handle(GetAllRolesQuery r, CancellationToken ct)
        => await _context.Roles.ToListAsync(ct);

    public async Task<Ent.Roles?> Handle(GetRolByIdQuery r, CancellationToken ct)
        => await _context.Roles.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Roles> Handle(CreateRolCommand r, CancellationToken ct)
    {
        _context.Roles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRolCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.Id) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRolCommand r, CancellationToken ct)
    {
        var item = await _context.Roles.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Roles.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
