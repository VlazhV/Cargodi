using Cargodi.Business.DTOs.Staff.Operator;

namespace Cargodi.Business.Interfaces.Staff;

public interface IOperatorService
{
	Task<GetOperatorDto> GetByIdAsync(int id);
	Task<GetOperatorDto> CreateAsync(UpdateOperatorDto updateOperatorDto, long userId, CancellationToken cancellationToken);
	Task FireAsync(int id);
}