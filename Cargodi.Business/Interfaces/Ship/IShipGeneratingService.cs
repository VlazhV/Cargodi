using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.Business.Interfaces.Ship;

public interface IShipGeneratingService
{
	//Task<IEnumerable<GetShipDto>> BuildRouteAsync(CancellationToken cancellationToken);
	Task<(Car, Trailer?)> SelectVehicleAsync(DataAccess.Entities.Ship.Ship ship, CancellationToken cancellationToken);
	Task<IEnumerable<Driver>> SelectDriversAsync(Car car, CancellationToken cancellationToken);
	
}