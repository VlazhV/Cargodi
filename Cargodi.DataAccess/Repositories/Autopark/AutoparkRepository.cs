using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Autopark;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Autopark;

public class AutoparkRepository : RepositoryBase<Entities.Autopark.Autopark, int>, IAutoparkRepository
{
    public AutoparkRepository(DatabaseContext db) : base(db)
    {
    }

    public async Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Autoparks
			.AsNoTracking()
			.AnyAsync(a => a.Id == id, cancellationToken);
    }
}
