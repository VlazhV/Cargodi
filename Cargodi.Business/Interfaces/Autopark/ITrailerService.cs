using Cargodi.Business.DTOs.Autopark.Trailer;

namespace Cargodi.Business.Interfaces.Autopark;

public interface ITrailerService
{
    Task<GetTrailerDto> CreateAsync(UpdateTrailerDto trailerDto, CancellationToken cancellationToken);
	Task DeleteAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<GetTrailerAutoparkDto>> GetAllAsync(CancellationToken cancellationToken);
	Task<GetTrailerAutoparkDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<GetTrailerDto> UpdateAsync(int id, UpdateTrailerDto trailerDto, CancellationToken cancellationToken);
}