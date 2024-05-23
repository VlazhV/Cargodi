using Cargodi.Business.DTOs.Autopark.Car;

namespace Cargodi.Business.Interfaces.Autopark;

public interface ICarService
{
    Task<GetCarDto> CreateAsync(UpdateCarDto carDto, CancellationToken cancellationToken);
	Task DeleteAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<GetCarDto>> GetAllAsync(CancellationToken cancellationToken);
	Task<GetCarDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<GetCarDto> UpdateAsync(int id, UpdateCarDto carDto, CancellationToken cancellationToken);
}