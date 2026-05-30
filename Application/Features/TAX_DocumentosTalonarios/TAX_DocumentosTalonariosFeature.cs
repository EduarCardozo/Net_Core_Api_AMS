using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_DocumentosTalonarios;

public record GetAllTAX_DocumentosTalonariosQuery : IRequest<IEnumerable<Ent.TAX_DocumentosTalonarios>>;
public record GetTAX_DocumentoTalonarioByIdQuery(int Id) : IRequest<Ent.TAX_DocumentosTalonarios?>;
public record CreateTAX_DocumentoTalonarioCommand(Ent.TAX_DocumentosTalonarios Item) : IRequest<Ent.TAX_DocumentosTalonarios>;
public record UpdateTAX_DocumentoTalonarioCommand(int Id, Ent.TAX_DocumentosTalonarios Item) : IRequest<bool>;
public record DeleteTAX_DocumentoTalonarioCommand(int Id) : IRequest<bool>;

public class TAX_DocumentosTalonariosHandlers :
    IRequestHandler<GetAllTAX_DocumentosTalonariosQuery, IEnumerable<Ent.TAX_DocumentosTalonarios>>,
    IRequestHandler<GetTAX_DocumentoTalonarioByIdQuery, Ent.TAX_DocumentosTalonarios?>,
    IRequestHandler<CreateTAX_DocumentoTalonarioCommand, Ent.TAX_DocumentosTalonarios>,
    IRequestHandler<UpdateTAX_DocumentoTalonarioCommand, bool>,
    IRequestHandler<DeleteTAX_DocumentoTalonarioCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_DocumentosTalonariosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_DocumentosTalonarios>> Handle(GetAllTAX_DocumentosTalonariosQuery r, CancellationToken ct)
        => await _context.TAX_DocumentosTalonarios.ToListAsync(ct);

    public async Task<Ent.TAX_DocumentosTalonarios?> Handle(GetTAX_DocumentoTalonarioByIdQuery r, CancellationToken ct)
        => await _context.TAX_DocumentosTalonarios.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_DocumentosTalonarios> Handle(CreateTAX_DocumentoTalonarioCommand r, CancellationToken ct)
    {
        _context.TAX_DocumentosTalonarios.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_DocumentoTalonarioCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.DocumentoTalonarioID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_DocumentoTalonarioCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_DocumentosTalonarios.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_DocumentosTalonarios.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
