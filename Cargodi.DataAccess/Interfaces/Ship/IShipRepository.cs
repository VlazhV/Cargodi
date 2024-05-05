namespace Cargodi.DataAccess.Interfaces.Ship;

public interface IShipRepository: IRepository<Entities.Ship.Ship, int>
{
    Task<bool> DoesItExistAsync(int Id, CancellationToken cancellationToken);
}