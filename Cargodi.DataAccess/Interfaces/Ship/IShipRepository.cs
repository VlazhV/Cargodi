namespace Cargodi.DataAccess.Interfaces.Ship;

public interface IShipRepository: IRepository<Entities.Ship.Ship, int>
{
    Task<bool> DoesItExistAsync(int Id, CancellationToken cancellationToken);

    Task<Entities.Ship.Ship?> GetShipWithStopsWithOrdersAsync(int id, CancellationToken cancellationToken);

    Task CreateManyAsync(IEnumerable<Entities.Ship.Ship> ships, CancellationToken cancellationToken);
}