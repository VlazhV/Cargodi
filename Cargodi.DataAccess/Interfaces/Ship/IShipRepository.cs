namespace Cargodi.DataAccess.Interfaces.Ship;

public interface IShipRepository: IRepository<Entities.Ship.Ship, int>
{
    Task<bool> DoesItExistAsync(int Id, CancellationToken cancellationToken);

    Task<Entities.Ship.Ship?> GetShipFullInfoByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Entities.Ship.Ship>> GetAllShipsFullInfoAsync(CancellationToken cancellationToken);

    Task CreateManyAsync(IEnumerable<Entities.Ship.Ship> ships, CancellationToken cancellationToken);

    Task RemoveAllStopsOfShipAsync(int shipId, CancellationToken cancellationToken);

    Task<IEnumerable<Entities.Ship.Ship>> GetShipsFullInfoOfDriverAsync(long userId, CancellationToken cancellationToken);
}