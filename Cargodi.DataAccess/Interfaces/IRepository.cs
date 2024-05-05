namespace Cargodi.DataAccess.Interfaces;

public interface IRepository<T, K> where T: class
{
	Task<T?> GetByIdAsync(K id, CancellationToken cancellationToken);
	Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
	Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
	T Update(T entity);
	T Delete(T entity);
	Task SaveChangesAsync(CancellationToken cancellationToken);
}