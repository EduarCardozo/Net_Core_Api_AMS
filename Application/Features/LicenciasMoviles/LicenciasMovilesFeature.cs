using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.LicenciasMoviles;

public record GetAllLicenciasMovilesQuery : IRequest<IEnumerable<Ent.LicenciasMoviles>>;
public record GetLicenciaMovilByIdQuery(int Id) : IRequest<Ent.LicenciasMoviles?>;
public record CreateLicenciaMovilCommand(Ent.LicenciasMoviles Item) : IRequest<Ent.LicenciasMoviles>;
public record UpdateLicenciaMovilCommand(int Id, Ent.LicenciasMoviles Item) : IRequest<bool>;
public record DeleteLicenciaMovilCommand(int Id) : IRequest<bool>;

public class LicenciasMovilesHandlers :
    IRequestHandler<GetAllLicenciasMovilesQuery, IEnumerable<Ent.LicenciasMoviles>>,
    IRequestHandler<GetLicenciaMovilByIdQuery, Ent.LicenciasMoviles?>,
    IRequestHandler<CreateLicenciaMovilCommand, Ent.LicenciasMoviles>,
    IRequestHandler<UpdateLicenciaMovilCommand, bool>,
    IRequestHandler<DeleteLicenciaMovilCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public LicenciasMovilesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.LicenciasMoviles>> Handle(GetAllLicenciasMovilesQuery r, CancellationToken ct)
        => await _context.LicenciasMoviles.ToListAsync(ct);

    public async Task<Ent.LicenciasMoviles?> Handle(GetLicenciaMovilByIdQuery r, CancellationToken ct)
        => await _context.LicenciasMoviles.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.LicenciasMoviles> Handle(CreateLicenciaMovilCommand r, CancellationToken ct)
    {
        _context.LicenciasMoviles.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateLicenciaMovilCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.LicenciaMovilID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteLicenciaMovilCommand r, CancellationToken ct)
    {
        var item = await _context.LicenciasMoviles.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.LicenciasMoviles.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
