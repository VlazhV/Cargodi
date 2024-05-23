using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Staff;

public class ClientRepository : RepositoryBase<Client, long>, IClientRepository
{
    public ClientRepository(DatabaseContext db) : base(db)
    {
    }


    public Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken)
    {
        return _db.Clients
            .AsNoTracking()
            .AnyAsync(c => c.Id == id, cancellationToken);
    }

}
