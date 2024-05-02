using Cargodi.DataAccess.Entities.Order;

namespace Cargodi.DataAccess.Interfaces.Order;

public interface IPayloadRepository: IRepository<Payload, long>
{
	Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken);
	Task CreateManyAsync(IEnumerable<Payload> payloads, CancellationToken cancellationToken);
}