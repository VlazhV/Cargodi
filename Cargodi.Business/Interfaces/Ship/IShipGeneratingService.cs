using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.DataAccess.Entities.Autopark;
using ShipSpace = Cargodi.DataAccess.Entities.Ship;
using Cargodi.DataAccess.Entities.Staff;
using System.Security.Claims;

namespace Cargodi.Business.Interfaces.Ship;

public interface IShipGeneratingService
{
    Task<List<DataAccess.Entities.Ship.Ship>> BuildRoutesAsync(CancellationToken cancellationToken);
	Task<IEnumerable<ICarrier>> SelectVehicleAsync(DataAccess.Entities.Ship.Ship ship, CancellationToken cancellationToken);
	Task<IEnumerable<Driver>> SelectDriversAsync(Car car, CancellationToken cancellationToken);
	
}