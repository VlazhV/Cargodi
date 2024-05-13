using Cargodi.DataAccess.Entities.Autopark;

namespace Cargodi.DataAccess.Interfaces.Autopark;

public interface ITrailerRepository: IRepository<Trailer, int>
{
	Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken);
	Task<bool> DoesItExistAsync(string licenseNumber, CancellationToken cancellationToken);
	
	Task<IEnumerable<Trailer>> GetFreeTrailersAsync(CancellationToken cancellationToken);
	Task<IEnumerable<Trailer>> GetSuitableTrailersOrderedAsync(int weight, int volume, int biggestLinearSize, int autoparkStartId, CancellationToken cancellationToken);
}