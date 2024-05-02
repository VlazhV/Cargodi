namespace Cargodi.DataAccess.Interfaces.Autopark;

public interface IAutoparkRepository: IRepository<Entities.Autopark.Autopark, int>
{
    Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken);
}