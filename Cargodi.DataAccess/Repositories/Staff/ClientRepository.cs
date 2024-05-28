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

    public async Task<Client?> CreateClientAsync(Client client, CancellationToken cancellationToken)
    {
        var entry = await _db.Clients
            .AddAsync(client, cancellationToken);

        return entry.Entity;
    }


    public Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken)
    {
        return _db.Clients
            .AsNoTracking()
            .AnyAsync(c => c.Id == id, cancellationToken);
    }

    public Task<Client?> GetClientByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        return _db.Clients
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Client>> GetClientsInfoAsync(CancellationToken cancellationToken)
    {
        return await _db.Clients
            .AsNoTracking()
            .Include(c => c.User)
            .ToListAsync(cancellationToken: cancellationToken);
    }

}
