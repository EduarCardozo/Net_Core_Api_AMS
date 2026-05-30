using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Configuraciones;

public record GetAllConfiguracionesQuery : IRequest<IEnumerable<Ent.Configuraciones>>;
public record GetConfiguracionByIdQuery(int Id) : IRequest<Ent.Configuraciones?>;
public record CreateConfiguracionCommand(Ent.Configuraciones Item) : IRequest<Ent.Configuraciones>;
public record UpdateConfiguracionCommand(int Id, Ent.Configuraciones Item) : IRequest<bool>;
public record DeleteConfiguracionCommand(int Id) : IRequest<bool>;

public class ConfiguracionesHandlers :
    IRequestHandler<GetAllConfiguracionesQuery, IEnumerable<Ent.Configuraciones>>,
    IRequestHandler<GetConfiguracionByIdQuery, Ent.Configuraciones?>,
    IRequestHandler<CreateConfiguracionCommand, Ent.Configuraciones>,
    IRequestHandler<UpdateConfiguracionCommand, bool>,
    IRequestHandler<DeleteConfiguracionCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public ConfiguracionesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Configuraciones>> Handle(GetAllConfiguracionesQuery r, CancellationToken ct)
        => await _context.Configuraciones.ToListAsync(ct);

    public async Task<Ent.Configuraciones?> Handle(GetConfiguracionByIdQuery r, CancellationToken ct)
        => await _context.Configuraciones.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Configuraciones> Handle(CreateConfiguracionCommand r, CancellationToken ct)
    {
        _context.Configuraciones.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateConfiguracionCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ConfiguracionID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteConfiguracionCommand r, CancellationToken ct)
    {
        var item = await _context.Configuraciones.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Configuraciones.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
