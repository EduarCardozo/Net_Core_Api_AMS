using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Proveedores;

public record GetAllProveedoresQuery : IRequest<IEnumerable<Ent.Proveedores>>;
public record GetProveedorByIdQuery(int Id) : IRequest<Ent.Proveedores?>;
public record CreateProveedorCommand(Ent.Proveedores Item) : IRequest<Ent.Proveedores>;
public record UpdateProveedorCommand(int Id, Ent.Proveedores Item) : IRequest<bool>;
public record DeleteProveedorCommand(int Id) : IRequest<bool>;

public class ProveedoresHandlers :
    IRequestHandler<GetAllProveedoresQuery, IEnumerable<Ent.Proveedores>>,
    IRequestHandler<GetProveedorByIdQuery, Ent.Proveedores?>,
    IRequestHandler<CreateProveedorCommand, Ent.Proveedores>,
    IRequestHandler<UpdateProveedorCommand, bool>,
    IRequestHandler<DeleteProveedorCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public ProveedoresHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Proveedores>> Handle(GetAllProveedoresQuery r, CancellationToken ct)
        => await _context.Proveedores.ToListAsync(ct);

    public async Task<Ent.Proveedores?> Handle(GetProveedorByIdQuery r, CancellationToken ct)
        => await _context.Proveedores.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Proveedores> Handle(CreateProveedorCommand r, CancellationToken ct)
    {
        _context.Proveedores.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateProveedorCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ProveedorID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteProveedorCommand r, CancellationToken ct)
    {
        var item = await _context.Proveedores.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Proveedores.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
