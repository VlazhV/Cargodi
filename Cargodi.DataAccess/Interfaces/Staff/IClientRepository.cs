using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Interfaces.Staff;

public interface IClientRepository: IRepository<Client, long>
{
    Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken);
}
