using MediatR;
using Microsoft.EntityFrameworkCore;
using Ent = Net_Core_Api.Domain.Entities;
using Net_Core_Api.Infrastructure.Persistence;

namespace Net_Core_Api.Application.Features.Connectors;

public record GetAllConnectorsQuery : IRequest<IEnumerable<Ent.Connectors>>;
public record GetConnectorByIdQuery(int Id) : IRequest<Ent.Connectors?>;
public record CreateConnectorCommand(Ent.Connectors Item) : IRequest<Ent.Connectors>;
public record UpdateConnectorCommand(int Id, Ent.Connectors Item) : IRequest<bool>;
public record DeleteConnectorCommand(int Id) : IRequest<bool>;

public class ConnectorsHandlers :
    IRequestHandler<GetAllConnectorsQuery, IEnumerable<Ent.Connectors>>,
    IRequestHandler<GetConnectorByIdQuery, Ent.Connectors?>,
    IRequestHandler<CreateConnectorCommand, Ent.Connectors>,
    IRequestHandler<UpdateConnectorCommand, bool>,
    IRequestHandler<DeleteConnectorCommand, bool>
{
    private readonly AmsFlotDbContext _context;
    public ConnectorsHandlers(AmsFlotDbContext context) => _context = context;

    public async Task<IEnumerable<Ent.Connectors>> Handle(GetAllConnectorsQuery r, CancellationToken ct)
        => await _context.Connectors.ToListAsync(ct);

    public async Task<Ent.Connectors?> Handle(GetConnectorByIdQuery r, CancellationToken ct)
        => await _context.Connectors.FindAsync(new object[] { r.Id }, ct);

    public async Task<Ent.Connectors> Handle(CreateConnectorCommand r, CancellationToken ct)
    {
        _context.Connectors.Add(r.Item);
        await _context.SaveChangesAsync(ct);
        return r.Item;
    }

    public async Task<bool> Handle(UpdateConnectorCommand r, CancellationToken ct)
    {
        if (r.Id != r.Item.ConnectorID) return false;
        _context.Entry(r.Item).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(DeleteConnectorCommand r, CancellationToken ct)
    {
        var item = await _context.Connectors.FindAsync(new object[] { r.Id }, ct);
        if (item == null) return false;
        _context.Connectors.Remove(item);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
