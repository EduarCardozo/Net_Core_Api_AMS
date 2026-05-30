using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.MarcasModelos;

public record GetAllMarcasModelosQuery : IRequest<IEnumerable<Ent.MarcasModelos>>;
public record GetMarcaModeloByIdQuery(int Id) : IRequest<Ent.MarcasModelos?>;
public record CreateMarcaModeloCommand(Ent.MarcasModelos Item) : IRequest<Ent.MarcasModelos>;
public record UpdateMarcaModeloCommand(int Id, Ent.MarcasModelos Item) : IRequest<bool>;
public record DeleteMarcaModeloCommand(int Id) : IRequest<bool>;

public class MarcasModelosHandlers :
    IRequestHandler<GetAllMarcasModelosQuery, IEnumerable<Ent.MarcasModelos>>,
    IRequestHandler<GetMarcaModeloByIdQuery, Ent.MarcasModelos?>,
    IRequestHandler<CreateMarcaModeloCommand, Ent.MarcasModelos>,
    IRequestHandler<UpdateMarcaModeloCommand, bool>,
    IRequestHandler<DeleteMarcaModeloCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public MarcasModelosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.MarcasModelos>> Handle(GetAllMarcasModelosQuery r, CancellationToken ct)
        => await _context.MarcasModelos.ToListAsync(ct);

    public async Task<Ent.MarcasModelos?> Handle(GetMarcaModeloByIdQuery r, CancellationToken ct)
        => await _context.MarcasModelos.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.MarcasModelos> Handle(CreateMarcaModeloCommand r, CancellationToken ct)
    {
        _context.MarcasModelos.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateMarcaModeloCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.MarcaModeloID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteMarcaModeloCommand r, CancellationToken ct)
    {
        var item = await _context.MarcasModelos.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.MarcasModelos.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
