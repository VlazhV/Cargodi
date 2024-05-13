using Cargodi.Business.DTOs.Ship.Ship;

namespace Cargodi.Business.Interfaces.Ship;

public interface IShipService
{
    Task<GetShipDto> MarkAsync(int shipId, CancellationToken cancellationToken);
}