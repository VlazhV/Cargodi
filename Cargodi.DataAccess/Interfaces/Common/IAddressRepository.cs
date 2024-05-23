using Cargodi.DataAccess.Entities;

namespace Cargodi.DataAccess.Interfaces.Common;

public interface IAddressRepository: IRepository<Address, long>
{
    public Task<Address?> GetIfExistsAsync(Address address, CancellationToken cancellationToken);
}