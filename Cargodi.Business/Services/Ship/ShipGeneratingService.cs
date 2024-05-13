using Cargodi.Business.Interfaces.Ship;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Interfaces.Order;
using Cargodi.DataAccess.Interfaces.Staff;

namespace Cargodi.Business.Services.Ship;

public class ShipGeneratingService : IShipGeneratingService
{
	private readonly ICarRepository _carRepository;
	private readonly ITrailerRepository _trailerRepository;
	private readonly IDriverRepository _driverRepository;
	private readonly IPayloadRepository _payloadRepository;
	
	public ShipGeneratingService(ICarRepository carRepository, ITrailerRepository trailerRepository, 
		IDriverRepository driverRepository, IPayloadRepository payloadRepository)
	{
		_carRepository = carRepository;
		_trailerRepository = trailerRepository;
		_driverRepository = driverRepository;
		_payloadRepository = payloadRepository;
	}

	public Task<DataAccess.Entities.Ship.Ship> BuildRouteAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Driver>> SelectDriversAsync(Car car, CancellationToken cancellationToken)
	{
		var categoriesToDrive = await _carRepository.GetCategoriesToDriveAsync(car, cancellationToken);
		var drivers = await _driverRepository.GetSuitableDriversAsync(categoriesToDrive, cancellationToken);

		if (!drivers.Any())
			throw new ArgumentException("Cannot find any driver to drive the car");

		var selectedDrivers = new List<Driver>
		{
			drivers.First()
		};
		
		if (drivers.Count() > 1)
			selectedDrivers.Add(drivers.Last());

		return selectedDrivers;
	}
	
	public async Task<(Car, Trailer?)> SelectVehicleAsync(DataAccess.Entities.Ship.Ship ship, CancellationToken cancellationToken)
	{
		var payloads = _payloadRepository.GetPayloadsOfShip(ship.Id);
		
		int volume = 0;
		int weight = 0;
		int biggestLinearSize = 0;
		
		foreach (Payload p in payloads)
		{
			volume += p.Length * p.Height * p.Width;
			weight += p.Weight;
			var maxPayloadSize = Math.Max(p.Width, Math.Max(p.Length, p.Height));
			biggestLinearSize = Math.Max(biggestLinearSize, maxPayloadSize);
		}

		var cars = await _carRepository
			.GetSuitableCarsOrderedAsync(weight, volume, biggestLinearSize, ship.AutoparkStartId, cancellationToken);
		
		var trailers = await _trailerRepository
			.GetSuitableTrailersOrderedAsync(weight, volume, biggestLinearSize, ship.AutoparkStartId, cancellationToken);
		
		List<ICarrier> carriers = new();	
		carriers.AddRange(cars);
		carriers.AddRange(trailers);
		
		if (carriers.Count == 0)
		{
			throw new ArgumentException("Cannot find vehicle");
		}

		var carrier = carriers.OrderBy(car => car.Capacity()).First();
		
		if (carrier.GetType() == typeof(Trailer))
		{
			var trucks = await _carRepository.GetCarsOfTypeAsync(CarTypes.Truck, cancellationToken);

			return (trucks.First(), (Trailer)carrier);
		}

		return ((Car)carrier, null);
	}

}