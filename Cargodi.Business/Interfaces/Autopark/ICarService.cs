using Cargodi.Business.DTOs.Autopark.Car;

namespace Cargodi.Business.Interfaces.Autopark;

public interface ICarService
{
    Task<GetCarDto> CreateAsync(UpdateCarDto carDto, CancellationToken cancellationToken);
	Task DeleteAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<GetCarAutoparkDto>> GetAllAsync(CancellationToken cancellationToken);
	Task<GetCarAutoparkDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<GetCarDto> UpdateAsync(int id, UpdateCarDto carDto, CancellationToken cancellationToken);
}