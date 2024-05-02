namespace Cargodi.DataAccess.Interfaces.Ship;

public interface IShipRepository: IRepository<Entities.Ship.Ship, int>
{
    Task<bool> DoesItExist(int Id, CancellationToken cancellationToken);
}