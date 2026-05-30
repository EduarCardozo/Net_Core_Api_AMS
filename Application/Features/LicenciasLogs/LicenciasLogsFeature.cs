using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.LicenciasLogs;

public record GetAllLicenciasLogsQuery : IRequest<IEnumerable<Ent.LicenciasLogs>>;
public record GetLicenciaLogByIdQuery(int Id) : IRequest<Ent.LicenciasLogs?>;
public record CreateLicenciaLogCommand(Ent.LicenciasLogs Item) : IRequest<Ent.LicenciasLogs>;
public record UpdateLicenciaLogCommand(int Id, Ent.LicenciasLogs Item) : IRequest<bool>;
public record DeleteLicenciaLogCommand(int Id) : IRequest<bool>;

public class LicenciasLogsHandlers :
    IRequestHandler<GetAllLicenciasLogsQuery, IEnumerable<Ent.LicenciasLogs>>,
    IRequestHandler<GetLicenciaLogByIdQuery, Ent.LicenciasLogs?>,
    IRequestHandler<CreateLicenciaLogCommand, Ent.LicenciasLogs>,
    IRequestHandler<UpdateLicenciaLogCommand, bool>,
    IRequestHandler<DeleteLicenciaLogCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public LicenciasLogsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.LicenciasLogs>> Handle(GetAllLicenciasLogsQuery r, CancellationToken ct)
        => await _context.LicenciasLogs.ToListAsync(ct);

    public async Task<Ent.LicenciasLogs?> Handle(GetLicenciaLogByIdQuery r, CancellationToken ct)
        => await _context.LicenciasLogs.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.LicenciasLogs> Handle(CreateLicenciaLogCommand r, CancellationToken ct)
    {
        _context.LicenciasLogs.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateLicenciaLogCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.LicenciaLogID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteLicenciaLogCommand r, CancellationToken ct)
    {
        var item = await _context.LicenciasLogs.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.LicenciasLogs.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
