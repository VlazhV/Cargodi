using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Interfaces.Staff;

public interface IDriverRepository : IRepository<Driver, int>
{
    Task<List<Driver>> GetSuitableDriversAsync(IEnumerable<Category> categories, int? shipId, CancellationToken cancellationToken);
    Task<Driver?> GetDriverByUserIdAsync(long userId, CancellationToken cancellationToken);

    Task<Driver?> CreateDriverAsync(Driver driver, CancellationToken cancellationToken);
    Task<IEnumerable<Driver>> GetDriversByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
    Task<IEnumerable<Driver>> GetAllAsync(bool? isFree,
        bool? works,
        int? actualAutoparkId,
        CancellationToken cancellationToken);
}