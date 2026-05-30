using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Especialidades;

public record GetAllEspecialidadesQuery : IRequest<IEnumerable<Ent.Especialidades>>;
public record GetEspecialidadByIdQuery(int Id) : IRequest<Ent.Especialidades?>;
public record CreateEspecialidadCommand(Ent.Especialidades Item) : IRequest<Ent.Especialidades>;
public record UpdateEspecialidadCommand(int Id, Ent.Especialidades Item) : IRequest<bool>;
public record DeleteEspecialidadCommand(int Id) : IRequest<bool>;

public class EspecialidadesHandlers :
    IRequestHandler<GetAllEspecialidadesQuery, IEnumerable<Ent.Especialidades>>,
    IRequestHandler<GetEspecialidadByIdQuery, Ent.Especialidades?>,
    IRequestHandler<CreateEspecialidadCommand, Ent.Especialidades>,
    IRequestHandler<UpdateEspecialidadCommand, bool>,
    IRequestHandler<DeleteEspecialidadCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public EspecialidadesHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Especialidades>> Handle(GetAllEspecialidadesQuery r, CancellationToken ct)
        => await _context.Especialidades.ToListAsync(ct);

    public async Task<Ent.Especialidades?> Handle(GetEspecialidadByIdQuery r, CancellationToken ct)
        => await _context.Especialidades.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Especialidades> Handle(CreateEspecialidadCommand r, CancellationToken ct)
    {
        _context.Especialidades.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateEspecialidadCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.EspecialidadID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteEspecialidadCommand r, CancellationToken ct)
    {
        var item = await _context.Especialidades.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Especialidades.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
