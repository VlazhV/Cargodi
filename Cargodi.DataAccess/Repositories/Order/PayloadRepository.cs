using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Order;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Order;

public class PayloadRepository : RepositoryBase<Payload, long>, IPayloadRepository
{
    public PayloadRepository(DatabaseContext db) : base(db)
    {
    }

    public async Task CreateManyAsync(IEnumerable<Payload> payloads, CancellationToken cancellationToken)
    {
        await _db.Payloads.AddRangeAsync(payloads, cancellationToken);
    }

    public async Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken)
    {
        return await _db.Payloads
			.AsNoTracking()
			.AnyAsync(p => p.Id == id, cancellationToken);
    }
}