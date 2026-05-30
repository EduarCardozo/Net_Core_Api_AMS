using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Licencias;

public record GetAllLicenciasQuery : IRequest<IEnumerable<Ent.Licencias>>;
public record GetLicenciaByIdQuery(int Id) : IRequest<Ent.Licencias?>;
public record CreateLicenciaCommand(Ent.Licencias Item) : IRequest<Ent.Licencias>;
public record UpdateLicenciaCommand(int Id, Ent.Licencias Item) : IRequest<bool>;
public record DeleteLicenciaCommand(int Id) : IRequest<bool>;

public class LicenciasHandlers :
    IRequestHandler<GetAllLicenciasQuery, IEnumerable<Ent.Licencias>>,
    IRequestHandler<GetLicenciaByIdQuery, Ent.Licencias?>,
    IRequestHandler<CreateLicenciaCommand, Ent.Licencias>,
    IRequestHandler<UpdateLicenciaCommand, bool>,
    IRequestHandler<DeleteLicenciaCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public LicenciasHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Licencias>> Handle(GetAllLicenciasQuery r, CancellationToken ct)
        => await _context.Licencias.ToListAsync(ct);

    public async Task<Ent.Licencias?> Handle(GetLicenciaByIdQuery r, CancellationToken ct)
        => await _context.Licencias.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Licencias> Handle(CreateLicenciaCommand r, CancellationToken ct)
    {
        _context.Licencias.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateLicenciaCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.LicenciaID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteLicenciaCommand r, CancellationToken ct)
    {
        var item = await _context.Licencias.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Licencias.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
