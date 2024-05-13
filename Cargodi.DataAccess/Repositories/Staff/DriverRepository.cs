using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Staff;

public class DriverRepository : RepositoryBase<Driver, int>, IDriverRepository
{
	public DriverRepository(DatabaseContext databaseContext): base(databaseContext)
	{	}
	public async Task<IEnumerable<Driver>> GetSuitableDriversAsync(IEnumerable<Category> categories, CancellationToken cancellationToken)
	{
		return await _db.Drivers
			.Include(d => d.Categories)
			.Where(d => d.Categories.Intersect(categories).Any())
			.ToListAsync(cancellationToken);
	}

}