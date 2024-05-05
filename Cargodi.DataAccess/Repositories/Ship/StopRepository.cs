using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Ship;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Ship;

namespace Cargodi.DataAccess.Repositories.Ship;

public class StopRepository : RepositoryBase<Stop, long>, IStopRepository
{
    public StopRepository(DatabaseContext db) : base(db)
    {
    }

}