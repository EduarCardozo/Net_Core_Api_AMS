using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Usuarios;

public record GetAllUsuariosQuery : IRequest<IEnumerable<Ent.Usuarios>>;
public record GetUsuarioByIdQuery(int Id) : IRequest<Ent.Usuarios?>;
public record CreateUsuarioCommand(Ent.Usuarios Item) : IRequest<Ent.Usuarios>;
public record UpdateUsuarioCommand(int Id, Ent.Usuarios Item) : IRequest<bool>;
public record DeleteUsuarioCommand(int Id) : IRequest<bool>;

public class UsuariosHandlers :
    IRequestHandler<GetAllUsuariosQuery, IEnumerable<Ent.Usuarios>>,
    IRequestHandler<GetUsuarioByIdQuery, Ent.Usuarios?>,
    IRequestHandler<CreateUsuarioCommand, Ent.Usuarios>,
    IRequestHandler<UpdateUsuarioCommand, bool>,
    IRequestHandler<DeleteUsuarioCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public UsuariosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Usuarios>> Handle(GetAllUsuariosQuery r, CancellationToken ct)
        => await _context.Usuarios.ToListAsync(ct);

    public async Task<Ent.Usuarios?> Handle(GetUsuarioByIdQuery r, CancellationToken ct)
        => await _context.Usuarios.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Usuarios> Handle(CreateUsuarioCommand r, CancellationToken ct)
    {
        _context.Usuarios.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateUsuarioCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.UsuarioID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteUsuarioCommand r, CancellationToken ct)
    {
        var item = await _context.Usuarios.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Usuarios.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
