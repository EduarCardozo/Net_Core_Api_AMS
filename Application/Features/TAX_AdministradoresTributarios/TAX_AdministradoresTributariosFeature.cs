using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.TAX_AdministradoresTributarios;

public record GetAllTAX_AdministradoresTributariosQuery : IRequest<IEnumerable<Ent.TAX_AdministradoresTributarios>>;
public record GetTAX_AdministradorTributarioByIdQuery(int Id) : IRequest<Ent.TAX_AdministradoresTributarios?>;
public record CreateTAX_AdministradorTributarioCommand(Ent.TAX_AdministradoresTributarios Item) : IRequest<Ent.TAX_AdministradoresTributarios>;
public record UpdateTAX_AdministradorTributarioCommand(int Id, Ent.TAX_AdministradoresTributarios Item) : IRequest<bool>;
public record DeleteTAX_AdministradorTributarioCommand(int Id) : IRequest<bool>;

public class TAX_AdministradoresTributariosHandlers :
    IRequestHandler<GetAllTAX_AdministradoresTributariosQuery, IEnumerable<Ent.TAX_AdministradoresTributarios>>,
    IRequestHandler<GetTAX_AdministradorTributarioByIdQuery, Ent.TAX_AdministradoresTributarios?>,
    IRequestHandler<CreateTAX_AdministradorTributarioCommand, Ent.TAX_AdministradoresTributarios>,
    IRequestHandler<UpdateTAX_AdministradorTributarioCommand, bool>,
    IRequestHandler<DeleteTAX_AdministradorTributarioCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TAX_AdministradoresTributariosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.TAX_AdministradoresTributarios>> Handle(GetAllTAX_AdministradoresTributariosQuery r, CancellationToken ct)
        => await _context.TAX_AdministradoresTributarios.ToListAsync(ct);

    public async Task<Ent.TAX_AdministradoresTributarios?> Handle(GetTAX_AdministradorTributarioByIdQuery r, CancellationToken ct)
        => await _context.TAX_AdministradoresTributarios.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.TAX_AdministradoresTributarios> Handle(CreateTAX_AdministradorTributarioCommand r, CancellationToken ct)
    {
        _context.TAX_AdministradoresTributarios.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTAX_AdministradorTributarioCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.AdministradorTributarioID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTAX_AdministradorTributarioCommand r, CancellationToken ct)
    {
        var item = await _context.TAX_AdministradoresTributarios.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.TAX_AdministradoresTributarios.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
