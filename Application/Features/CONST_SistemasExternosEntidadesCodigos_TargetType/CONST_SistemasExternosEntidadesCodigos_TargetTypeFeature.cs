using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.CONST_SistemasExternosEntidadesCodigos_TargetType;

public record GetAllCONST_SistemasExternosEntidadesCodigos_TargetTypeQuery : IRequest<IEnumerable<Ent.CONST_SistemasExternosEntidadesCodigos_TargetType>>;
public record GetCONST_TargetTypeByIdQuery(byte Id) : IRequest<Ent.CONST_SistemasExternosEntidadesCodigos_TargetType?>;
public record CreateCONST_TargetTypeCommand(Ent.CONST_SistemasExternosEntidadesCodigos_TargetType Item) : IRequest<Ent.CONST_SistemasExternosEntidadesCodigos_TargetType>;
public record UpdateCONST_TargetTypeCommand(byte Id, Ent.CONST_SistemasExternosEntidadesCodigos_TargetType Item) : IRequest<bool>;
public record DeleteCONST_TargetTypeCommand(byte Id) : IRequest<bool>;

public class CONST_SistemasExternosEntidadesCodigos_TargetTypeHandlers :
    IRequestHandler<GetAllCONST_SistemasExternosEntidadesCodigos_TargetTypeQuery, IEnumerable<Ent.CONST_SistemasExternosEntidadesCodigos_TargetType>>,
    IRequestHandler<GetCONST_TargetTypeByIdQuery, Ent.CONST_SistemasExternosEntidadesCodigos_TargetType?>,
    IRequestHandler<CreateCONST_TargetTypeCommand, Ent.CONST_SistemasExternosEntidadesCodigos_TargetType>,
    IRequestHandler<UpdateCONST_TargetTypeCommand, bool>,
    IRequestHandler<DeleteCONST_TargetTypeCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public CONST_SistemasExternosEntidadesCodigos_TargetTypeHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.CONST_SistemasExternosEntidadesCodigos_TargetType>> Handle(GetAllCONST_SistemasExternosEntidadesCodigos_TargetTypeQuery r, CancellationToken ct)
        => await _context.CONST_SistemasExternosEntidadesCodigos_TargetType.ToListAsync(ct);

    public async Task<Ent.CONST_SistemasExternosEntidadesCodigos_TargetType?> Handle(GetCONST_TargetTypeByIdQuery r, CancellationToken ct)
        => await _context.CONST_SistemasExternosEntidadesCodigos_TargetType.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.CONST_SistemasExternosEntidadesCodigos_TargetType> Handle(CreateCONST_TargetTypeCommand r, CancellationToken ct)
    {
        _context.CONST_SistemasExternosEntidadesCodigos_TargetType.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateCONST_TargetTypeCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.TargetTypeId) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteCONST_TargetTypeCommand r, CancellationToken ct)
    {
        var item = await _context.CONST_SistemasExternosEntidadesCodigos_TargetType.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.CONST_SistemasExternosEntidadesCodigos_TargetType.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
