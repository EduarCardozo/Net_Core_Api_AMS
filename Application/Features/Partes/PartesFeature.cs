using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Partes;

public record GetAllPartesQuery : IRequest<IEnumerable<Ent.Partes>>;
public record GetParteByIdQuery(int Id) : IRequest<Ent.Partes?>;
public record CreateParteCommand(Ent.Partes Item) : IRequest<Ent.Partes>;
public record UpdateParteCommand(int Id, Ent.Partes Item) : IRequest<bool>;
public record DeleteParteCommand(int Id) : IRequest<bool>;

public class PartesHandlers :
    IRequestHandler<GetAllPartesQuery, IEnumerable<Ent.Partes>>,
    IRequestHandler<GetParteByIdQuery, Ent.Partes?>,
    IRequestHandler<CreateParteCommand, Ent.Partes>,
    IRequestHandler<UpdateParteCommand, bool>,
    IRequestHandler<DeleteParteCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public PartesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Partes>> Handle(GetAllPartesQuery r, CancellationToken ct)
        => await _context.Partes.ToListAsync(ct);

    public async Task<Ent.Partes?> Handle(GetParteByIdQuery r, CancellationToken ct)
        => await _context.Partes.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Partes> Handle(CreateParteCommand r, CancellationToken ct)
    {
        _context.Partes.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateParteCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ParteID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteParteCommand r, CancellationToken ct)
    {
        var item = await _context.Partes.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Partes.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
