using Cargodi.DataAccess.Entities.Autopark;

namespace Cargodi.DataAccess.Interfaces.Autopark;

public interface ICarRepository: IRepository<Car, int>
{
	Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken);
	Task<bool> DoesItExistAsync(string licenseNumber, CancellationToken cancellationToken);
}