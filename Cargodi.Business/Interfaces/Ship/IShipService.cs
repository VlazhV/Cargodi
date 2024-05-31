using System.Security.Claims;
using Cargodi.Business.DTOs.Ship.Ship;

namespace Cargodi.Business.Interfaces.Ship;

public interface IShipService
{
    Task<GetShipDto> MarkAsync(int shipId, ClaimsPrincipal user, CancellationToken cancellationToken);
    Task<GetShipDto> GetByIdAsync(int shipId, CancellationToken cancellationToken);
    Task<IEnumerable<GetShipDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<GetShipDto>> GenerateAsync(ClaimsPrincipal user, int driversCount, CancellationToken cancellationToken);
    Task<GetShipDto> UpdateAsync(int shipId, UpdateShipDto shipDto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<GetShipDto>> GetAllOfDriverAsync(long userId, CancellationToken cancellationToken);
}