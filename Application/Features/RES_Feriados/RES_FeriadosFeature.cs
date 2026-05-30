using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.RES_Feriados;

public record GetAllRES_FeriadosQuery : IRequest<IEnumerable<Ent.RES_Feriados>>;
public record GetRES_FeriadoByIdQuery(int Id) : IRequest<Ent.RES_Feriados?>;
public record CreateRES_FeriadoCommand(Ent.RES_Feriados Item) : IRequest<Ent.RES_Feriados>;
public record UpdateRES_FeriadoCommand(int Id, Ent.RES_Feriados Item) : IRequest<bool>;
public record DeleteRES_FeriadoCommand(int Id) : IRequest<bool>;

public class RES_FeriadosHandlers :
    IRequestHandler<GetAllRES_FeriadosQuery, IEnumerable<Ent.RES_Feriados>>,
    IRequestHandler<GetRES_FeriadoByIdQuery, Ent.RES_Feriados?>,
    IRequestHandler<CreateRES_FeriadoCommand, Ent.RES_Feriados>,
    IRequestHandler<UpdateRES_FeriadoCommand, bool>,
    IRequestHandler<DeleteRES_FeriadoCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public RES_FeriadosHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.RES_Feriados>> Handle(GetAllRES_FeriadosQuery r, CancellationToken ct)
        => await _context.RES_Feriados.ToListAsync(ct);

    public async Task<Ent.RES_Feriados?> Handle(GetRES_FeriadoByIdQuery r, CancellationToken ct)
        => await _context.RES_Feriados.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.RES_Feriados> Handle(CreateRES_FeriadoCommand r, CancellationToken ct)
    {
        _context.RES_Feriados.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateRES_FeriadoCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.FeriadoID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteRES_FeriadoCommand r, CancellationToken ct)
    {
        var item = await _context.RES_Feriados.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.RES_Feriados.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
