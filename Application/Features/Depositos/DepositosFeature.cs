using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Depositos;

public record GetAllDepositosQuery : IRequest<IEnumerable<Ent.Depositos>>;
public record GetDepositoByIdQuery(int Id) : IRequest<Ent.Depositos?>;
public record CreateDepositoCommand(Ent.Depositos Item) : IRequest<Ent.Depositos>;
public record UpdateDepositoCommand(int Id, Ent.Depositos Item) : IRequest<bool>;
public record DeleteDepositoCommand(int Id) : IRequest<bool>;

public class DepositosHandlers :
    IRequestHandler<GetAllDepositosQuery, IEnumerable<Ent.Depositos>>,
    IRequestHandler<GetDepositoByIdQuery, Ent.Depositos?>,
    IRequestHandler<CreateDepositoCommand, Ent.Depositos>,
    IRequestHandler<UpdateDepositoCommand, bool>,
    IRequestHandler<DeleteDepositoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public DepositosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Depositos>> Handle(GetAllDepositosQuery r, CancellationToken ct)
        => await _context.Depositos.ToListAsync(ct);

    public async Task<Ent.Depositos?> Handle(GetDepositoByIdQuery r, CancellationToken ct)
        => await _context.Depositos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Depositos> Handle(CreateDepositoCommand r, CancellationToken ct)
    {
        _context.Depositos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateDepositoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.DepositoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteDepositoCommand r, CancellationToken ct)
    {
        var item = await _context.Depositos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Depositos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
