using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Comentarios;

public record GetAllComentariosQuery : IRequest<IEnumerable<Ent.Comentarios>>;
public record GetComentarioByIdQuery(int Id) : IRequest<Ent.Comentarios?>;
public record CreateComentarioCommand(Ent.Comentarios Item) : IRequest<Ent.Comentarios>;
public record UpdateComentarioCommand(int Id, Ent.Comentarios Item) : IRequest<bool>;
public record DeleteComentarioCommand(int Id) : IRequest<bool>;

public class ComentariosHandlers :
    IRequestHandler<GetAllComentariosQuery, IEnumerable<Ent.Comentarios>>,
    IRequestHandler<GetComentarioByIdQuery, Ent.Comentarios?>,
    IRequestHandler<CreateComentarioCommand, Ent.Comentarios>,
    IRequestHandler<UpdateComentarioCommand, bool>,
    IRequestHandler<DeleteComentarioCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public ComentariosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Comentarios>> Handle(GetAllComentariosQuery r, CancellationToken ct)
        => await _context.Comentarios.ToListAsync(ct);

    public async Task<Ent.Comentarios?> Handle(GetComentarioByIdQuery r, CancellationToken ct)
        => await _context.Comentarios.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Comentarios> Handle(CreateComentarioCommand r, CancellationToken ct)
    {
        _context.Comentarios.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateComentarioCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ComentarioID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteComentarioCommand r, CancellationToken ct)
    {
        var item = await _context.Comentarios.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Comentarios.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
