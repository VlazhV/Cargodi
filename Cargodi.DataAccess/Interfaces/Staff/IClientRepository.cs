using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Interfaces.Staff;

public interface IClientRepository: IRepository<Client, long>
{
    Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken);
    Task<Client?> GetClientByUserIdAsync(long userId, CancellationToken cancellationToken);

    Task<Client?> CreateClientAsync(Client client, CancellationToken cancellationToken);
    Task<IEnumerable<Client>> GetClientsInfoAsync(CancellationToken cancellationToken);
}
