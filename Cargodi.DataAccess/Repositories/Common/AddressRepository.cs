using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Common;

public class AddressRepository : RepositoryBase<Address, long>, IAddressRepository
{
	public AddressRepository(DatabaseContext db) : base(db)
	{
	}

	public Task<Address?> GetIfExistsAsync(Address address, CancellationToken cancellationToken)
	{
		return _db.Addresses
			.FirstOrDefaultAsync(a => a.Equals(address), cancellationToken);
	}
}