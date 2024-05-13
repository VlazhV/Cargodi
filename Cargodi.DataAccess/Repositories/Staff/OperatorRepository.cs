using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Staff;

public class OperatorRepository : RepositoryBase<Operator, int>, IOperatorRepository
{
	public OperatorRepository(DatabaseContext db) : base(db)
	{
	}

	public Task<Operator?> GetWithUserByIdAsync(int id, CancellationToken cancellationToken)
	{
		return _db.Operators
			.Include(o => o.User)
			.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);			
	}

}