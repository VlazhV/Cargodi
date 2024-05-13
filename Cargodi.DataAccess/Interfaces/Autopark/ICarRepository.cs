using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Interfaces.Autopark;

public interface ICarRepository: IRepository<Car, int>
{
	Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken);
	Task<bool> DoesItExistAsync(string licenseNumber, CancellationToken cancellationToken);
	Task<IEnumerable<Car>> GetFreeCarsAsync(CancellationToken cancellationToken);
	Task<IEnumerable<Car>> GetSuitableCarsOrderedAsync(int weight, int volume, int totalLinearSize, int autoparkStartId, CancellationToken cancellationToken);
	Task<IEnumerable<Car>> GetCarsOfTypeAsync(CarType carType, CancellationToken cancellationToken);

	Task<IEnumerable<Category>> GetCategoriesToDriveAsync(Car car, CancellationToken cancellationToken);
}