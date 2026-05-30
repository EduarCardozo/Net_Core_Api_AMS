using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.ProductVersion;

public record GetAllProductVersionQuery : IRequest<IEnumerable<Ent.ProductVersion>>;
public record GetProductVersionByIdQuery(int Id) : IRequest<Ent.ProductVersion?>;
public record CreateProductVersionCommand(Ent.ProductVersion Item) : IRequest<Ent.ProductVersion>;
public record UpdateProductVersionCommand(int Id, Ent.ProductVersion Item) : IRequest<bool>;
public record DeleteProductVersionCommand(int Id) : IRequest<bool>;

public class ProductVersionHandlers :
    IRequestHandler<GetAllProductVersionQuery, IEnumerable<Ent.ProductVersion>>,
    IRequestHandler<GetProductVersionByIdQuery, Ent.ProductVersion?>,
    IRequestHandler<CreateProductVersionCommand, Ent.ProductVersion>,
    IRequestHandler<UpdateProductVersionCommand, bool>,
    IRequestHandler<DeleteProductVersionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public ProductVersionHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.ProductVersion>> Handle(GetAllProductVersionQuery r, CancellationToken ct)
        => await _context.ProductVersion.ToListAsync(ct);

    public async Task<Ent.ProductVersion?> Handle(GetProductVersionByIdQuery r, CancellationToken ct)
        => await _context.ProductVersion.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.ProductVersion> Handle(CreateProductVersionCommand r, CancellationToken ct)
    {
        _context.ProductVersion.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateProductVersionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ProductoVersionId) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteProductVersionCommand r, CancellationToken ct)
    {
        var item = await _context.ProductVersion.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.ProductVersion.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
