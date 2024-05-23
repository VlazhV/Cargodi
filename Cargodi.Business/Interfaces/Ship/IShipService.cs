using Cargodi.Business.DTOs.Ship.Ship;

namespace Cargodi.Business.Interfaces.Ship;

public interface IShipService
{
    Task<GetShipDto> MarkAsync(int shipId, CancellationToken cancellationToken);
    Task<GetShipDto> GetByIdAsync(int shipId, CancellationToken cancellationToken);
    Task<IEnumerable<GetShipDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<GetShipDto>> GenerateAsync(CancellationToken cancellationToken);
    Task<GetShipDto> UpdateAsync(int shipId, UpdateShipDto shipDto, CancellationToken cancellationToken);
}