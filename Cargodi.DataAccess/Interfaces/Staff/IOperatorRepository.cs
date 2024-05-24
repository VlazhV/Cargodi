using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Interfaces.Staff;

public interface IOperatorRepository: IRepository<Operator, int>
{
    Task<Operator?> GetWithUserByIdAsync(int id, CancellationToken cancellationToken);

    Task<Operator?> GetOperatorByUserIdAsync(long userId, CancellationToken cancellationToken);

    Task<Operator?> CreateOperatorAsync(Operator @operator, CancellationToken cancellationToken);
}