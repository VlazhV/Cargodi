namespace Cargodi.DataAccess.Interfaces.Autopark;

public interface IAutoparkRepository: IRepository<Entities.Autopark.Autopark, int>
{
	Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<Entities.Autopark.Autopark>> GetAutoparksWithAddressesVehicleAsync(CancellationToken cancellationToken);
	Task<Entities.Autopark.Autopark?> GetAutoparkWithAddressesVehicleByIdAsync(int id, CancellationToken cancellationToken);
}