
using Cargodi.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Interfaces;

public class RepositoryBase<T, K> : IRepository<T, K> where T: class
{
protected readonly DatabaseContext _db;
	
	public RepositoryBase(DatabaseContext db)
	{
		_db = db;
	}
	
	public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
	{
        var entry = await _db.AddAsync(entity!, cancellationToken);
       
        return entry.Entity;
	}

	public T Delete(T entity)
	{
		var entry = _db.Remove(entity!);

		return entry.Entity;
	}

	public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _db.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
	}

    public async Task<T?> GetByIdAsync(K id, CancellationToken cancellationToken)
	{
		return await _db.Set<T>().FindAsync(id, cancellationToken);
	}

	public async Task SaveChangesAsync(CancellationToken cancellationToken)
	{
		await _db.SaveChangesAsync(cancellationToken);
	}

	public T Update(T entity)
	{
		var entry = _db.Update(entity);

		return entry.Entity;
	}

}