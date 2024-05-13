using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

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

	public IEnumerable<Payload> GetPayloadsOfShip(int shipId)
	{
		return _db.Ships
			.Include(s => s.Stops)
				.ThenInclude(st => st.Order)
					.ThenInclude(o => o.Payloads)
			.Where(ship => ship.Id == shipId)
			.SelectMany(c => c.Stops)
				.Select(st => st.Order)
			.SelectMany(o => o.Payloads);
	}
}