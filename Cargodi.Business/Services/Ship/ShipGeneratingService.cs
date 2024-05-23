using AutoMapper;
using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.Business.Interfaces.Ship;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Interfaces.Order;
using Cargodi.DataAccess.Interfaces.Ship;
using Cargodi.DataAccess.Interfaces.Staff;

namespace Cargodi.Business.Services.Ship;

public class ShipGeneratingService : IShipGeneratingService
{
	private readonly ICarRepository _carRepository;
	private readonly ITrailerRepository _trailerRepository;
	private readonly IDriverRepository _driverRepository;
	private readonly IPayloadRepository _payloadRepository;
	private readonly IOrderRepository _orderRepository;
	private readonly IAutoparkRepository _autoparkRepository;
	private readonly IShipRepository _shipRepository;
	private readonly IMapper _mapper;
	
	public ShipGeneratingService(ICarRepository carRepository, ITrailerRepository trailerRepository, 
		IDriverRepository driverRepository, IPayloadRepository payloadRepository,
		IOrderRepository orderRepository, IAutoparkRepository autoparkRepository,
		IShipRepository shipRepository,
		IMapper mapper)
	{
		_carRepository = carRepository;
		_trailerRepository = trailerRepository;
		_driverRepository = driverRepository;
		_payloadRepository = payloadRepository;
		_orderRepository = orderRepository;
		_autoparkRepository = autoparkRepository;
		_shipRepository = shipRepository;
		
		_mapper = mapper;
	}

	// public async Task<IEnumerable<GetShipDto>> BuildRouteAsync(CancellationToken cancellationToken)
	// {
	// 	var orders = await _orderRepository.GetOrdersWithPayloadsAsync(cancellationToken);
	// 	var autoparks = await _autoparkRepository.GetAutoparksWithAddressesAsync(cancellationToken);
	// }

	public async Task<IEnumerable<Driver>> SelectDriversAsync(Car car, CancellationToken cancellationToken)
	{
		var categoriesToDrive = await _carRepository.GetCategoriesToDriveAsync(car, cancellationToken);
		var drivers = await _driverRepository.GetSuitableDriversAsync(categoriesToDrive, cancellationToken);

        return drivers;
	}
	
	public async Task<IEnumerable<ICarrier>> SelectVehicleAsync(DataAccess.Entities.Ship.Ship ship, CancellationToken cancellationToken)
	{
		var shipfull = await _shipRepository.GetShipFullInfoByIdAsync(ship.Id, cancellationToken);
		var orders = shipfull!.Stops.Select(stop => stop.Order).ToList();
		
		int volume = FindMaxValue(orders, ValueType.Volume);
		int weight = FindMaxValue(orders, ValueType.Weight);

		var payloads = orders.SelectMany(o => o.Payloads);

		int biggestLinearSize = 0;
		
		foreach (Payload p in payloads)
		{
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

        return carriers;
	}
	
	private static int FindMaxValue(List<DataAccess.Entities.Order.Order> orders, ValueType valueType)
	{
		int[] sumsFromBegin = new int[orders.Count];
		
		int currentSum = 0;
		
		if (valueType == ValueType.Weight)
		{
			for (int i = 0; i < sumsFromBegin.Length; i++)
			{
				int orderSum = 0;
				foreach (var p in orders[i].Payloads)
				{
					orderSum += p.Weight;
				}

				currentSum += orderSum;
				sumsFromBegin[i] = currentSum;
			}
		}
		else
		{
			for (int i = 0; i < sumsFromBegin.Length; i++)
			{
				int orderSum = 0;
				foreach (var p in orders[i].Payloads)
				{
					orderSum += p.Width * p.Height * p.Length;
				}

				currentSum += orderSum;
				sumsFromBegin[i] = currentSum;
			}
		}
		
		int maxValue = 0;
		
		for (int i = 0; i < orders.Count; i++)
		{
			for (int j = 0; j <= i; j++)
			{
				int value = sumsFromBegin[i] - sumsFromBegin[j];

				maxValue = Math.Max(maxValue, value);
			}
		}

		return maxValue;
	}
}

public enum ValueType
{
	Weight,
	Volume
}