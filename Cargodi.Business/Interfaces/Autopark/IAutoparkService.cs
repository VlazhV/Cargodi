using Cargodi.Business.DTOs.Autopark.Autopark;

namespace Cargodi.Business.Interfaces.Autopark;

public interface IAutoparkService
{
    Task<GetAutoparkDto> CreateAsync(UpdateAutoparkDto autoparkDto, CancellationToken cancellationToken);
	Task DeleteAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<GetAutoparkVehicleDto>> GetAllAsync(CancellationToken cancellationToken);
	Task<GetAutoparkVehicleDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<GetAutoparkDto> UpdateAsync(int id, UpdateAutoparkDto autoparkDto, CancellationToken cancellationToken);
}