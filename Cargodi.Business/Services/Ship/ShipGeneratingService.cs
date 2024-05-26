using AutoMapper;
using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.Business.Interfaces.Ship;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Entities.Ship;
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

	// public async Task<IEnumerable<DataAccess.Entities.Ship.Ship>> BuildRoutesAsync(CancellationToken cancellationToken)
	// {
	//     var orders = (await _orderRepository.GetOrdersWithPayloadsAsync(cancellationToken)).ToList();
	//     var autoparks = await _autoparkRepository.GetAutoparksWithAddressesVehicleAsync(cancellationToken);

	//     int nOrders = orders.Count;
	  
	//     var routes = new List<ListNode>();
	//     var distanceMap = new double[nOrders * 2, nOrders * 2];
		
	//     var orderStarts = orders.ConvertAll(o => o.LoadAddress);
	//     var orderFinishes = orders.ConvertAll(o => o.DeliverAddress);
		
	//     FillDistanceMap(ref distanceMap, orderStarts, orderFinishes, nOrders);

	//     var pointAvailability = new bool[2 * nOrders];
		
	//     for (int i = 0; i < 2 * nOrders; i++)
	//     {
	//         pointAvailability[i] = i < nOrders;
	//     }

	//     var distanceMapCopy = new double[nOrders, nOrders];
	//     Array.Copy(distanceMap, distanceMapCopy, 2 * nOrders);
		
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
	
	private double CountDistance(Address from, Address to)
	{
		var deltaLatitude = ToRadians(Math.Abs(from.Latitude - to.Latitude));
		var deltaLongitude = ToRadians(Math.Abs(from.Longitude - to.Longitude));
		
		var sin2Lat = Math.Pow(Math.Sin(deltaLatitude / 2), 2);
		var sin2Long = Math.Pow(Math.Sin(deltaLongitude/ 2), 2);

		var a = sin2Lat + Math.Cos(ToRadians(from.Latitude)) * Math.Cos(ToRadians(to.Latitude)) * sin2Long;

		var distance = 2 * Math.Atan(Math.Sqrt(a) / (1 - Math.Sqrt(a)));
		return distance;
	}
	
	private double ToRadians(double degrees)
	{
		return degrees * Math.PI / 180;
	}
	
	private void FillDistanceMap(ref double[,] distanceMap, List<Address> orderStarts, List<Address> orderFinishes, int nOrders)
	{
		for (int i = 0; i < 2 * nOrders; i++)
		{
			for (int j = 0; j < i; j++)
			{
				if (i == j)
				{
					distanceMap[i, j] = double.PositiveInfinity; 
				}
				else 
				{
					Address address2 = i < nOrders ? orderStarts[i] : orderFinishes[i % nOrders];
					Address address1 = j < nOrders ? orderStarts[j] : orderFinishes[j % nOrders];

					var distance = CountDistance(address1, address2);
					distanceMap[j, i] = distance;
					
					if (i == j + nOrders)
					{
						distanceMap[i, j] = double.PositiveInfinity;
					}
					else 
					{
						distanceMap[i, j] = distance;
					}
				}
			}
		}
	}
}


public enum ValueType
{
	Weight,
	Volume
}

public class ListNode
{
	public Stop Stop { get; set; }
	public ListNode Next { get; set; }
}