using Cargodi.Business.DTOs.Autopark.Trailer;

namespace Cargodi.Business.Interfaces.Autopark;

public interface ITrailerService
{
    Task<GetTrailerDto> CreateAsync(UpdateTrailerDto trailerDto, CancellationToken cancellationToken);
	Task DeleteAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<GetTrailerDto>> GetAllAsync(CancellationToken cancellationToken);
	Task<GetTrailerDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<GetTrailerDto> UpdateAsync(int id, UpdateTrailerDto trailerDto, CancellationToken cancellationToken);
}