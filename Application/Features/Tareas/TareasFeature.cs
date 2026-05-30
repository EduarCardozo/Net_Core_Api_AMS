using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Tareas;

public record GetAllTareasQuery : IRequest<IEnumerable<Ent.Tareas>>;
public record GetTareaByIdQuery(int Id) : IRequest<Ent.Tareas?>;
public record CreateTareaCommand(Ent.Tareas Item) : IRequest<Ent.Tareas>;
public record UpdateTareaCommand(int Id, Ent.Tareas Item) : IRequest<bool>;
public record DeleteTareaCommand(int Id) : IRequest<bool>;

public class TareasHandlers :
    IRequestHandler<GetAllTareasQuery, IEnumerable<Ent.Tareas>>,
    IRequestHandler<GetTareaByIdQuery, Ent.Tareas?>,
    IRequestHandler<CreateTareaCommand, Ent.Tareas>,
    IRequestHandler<UpdateTareaCommand, bool>,
    IRequestHandler<DeleteTareaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public TareasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Tareas>> Handle(GetAllTareasQuery r, CancellationToken ct)
        => await _context.Tareas.ToListAsync(ct);

    public async Task<Ent.Tareas?> Handle(GetTareaByIdQuery r, CancellationToken ct)
        => await _context.Tareas.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Tareas> Handle(CreateTareaCommand r, CancellationToken ct)
    {
        _context.Tareas.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateTareaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.TareaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteTareaCommand r, CancellationToken ct)
    {
        var item = await _context.Tareas.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Tareas.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
