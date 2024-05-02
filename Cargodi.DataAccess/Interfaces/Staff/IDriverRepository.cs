using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Interfaces.Staff;

public interface IDriverRepository : IRepository<Driver, int>
{
	Task<IEnumerable<Driver>> GetAvailableAsync(CancellationToken cancellationToken);
}