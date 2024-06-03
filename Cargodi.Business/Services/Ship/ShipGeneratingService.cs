using AutoMapper;
using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.Business.Interfaces.Ship;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Autopark;
using OrderSpace = Cargodi.DataAccess.Entities.Order;
using ShipSpace = Cargodi.DataAccess.Entities.Ship;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Interfaces.Order;
using Cargodi.DataAccess.Interfaces.Ship;
using Cargodi.DataAccess.Interfaces.Staff;
using Cargodi.DataAccess.Entities.Order;
using System.Linq;
using Cargodi.DataAccess.Entities.Ship;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;
using Cargodi.Business.Exceptions;

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
    private readonly IOperatorRepository _operatorRepository;
    private readonly IMapper _mapper;
    
    public ShipGeneratingService(ICarRepository carRepository, ITrailerRepository trailerRepository, 
        IDriverRepository driverRepository, IPayloadRepository payloadRepository,
        IOrderRepository orderRepository, IAutoparkRepository autoparkRepository,
        IShipRepository shipRepository, IOperatorRepository operatorRepository,
        IMapper mapper)
    {
        _carRepository = carRepository;
        _trailerRepository = trailerRepository;
        _driverRepository = driverRepository;
        _payloadRepository = payloadRepository;
        _orderRepository = orderRepository;
        _autoparkRepository = autoparkRepository;
        _shipRepository = shipRepository;
        _operatorRepository = operatorRepository;
        
        _mapper = mapper;
    }

    public async Task<IEnumerable<Driver>> SelectDriversAsync(Car car, CancellationToken cancellationToken)
    {
        var categoriesToDrive = await _carRepository.GetCategoriesToDriveAsync(car, cancellationToken);
        var drivers = await _driverRepository.GetSuitableDriversAsync(categoriesToDrive, null, cancellationToken);
        
        return drivers;
    }
    
    public async Task<IEnumerable<ICarrier>> SelectVehicleAsync(DataAccess.Entities.Ship.Ship ship, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllOfShipAsync(ship, cancellationToken);
        
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
            .GetSuitableCarsOrderedAsync(weight, volume, biggestLinearSize, ship.AutoparkStartId, null, cancellationToken);
        
        var trailers = await _trailerRepository
            .GetSuitableTrailersOrderedAsync(weight, volume, biggestLinearSize, ship.AutoparkStartId, null, cancellationToken);
        
        List<ICarrier> carriers = new();	
        carriers.AddRange(cars.ToList());
        carriers.AddRange(trailers.ToList());

        return carriers;
    }
    
    private static int FindMaxValue(List<DataAccess.Entities.Order.Order> orders, ValueType valueType)
    {
        int[] sumsFromBegin = new int[orders.Count];
        
        int currentSum = 0;
        
        int maxValue = 0;
        
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
                maxValue = Math.Max(maxValue, currentSum);
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
                maxValue = Math.Max(maxValue, currentSum);
            }
        }
        
        
        
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
        const double R = 6400;
        
        var deltaLatitude = ToRadians(Math.Abs(from.Latitude - to.Latitude));
        var deltaLongitude = ToRadians(Math.Abs(from.Longitude - to.Longitude));
        
        var sin2Lat = Math.Pow(Math.Sin(deltaLatitude / 2), 2);
        var sin2Long = Math.Pow(Math.Sin(deltaLongitude/ 2), 2);

        var a = sin2Lat + Math.Cos(ToRadians(from.Latitude)) * Math.Cos(ToRadians(to.Latitude)) * sin2Long;

        var distance = 2 * R * Math.Atan(Math.Sqrt(a) / (1 - Math.Sqrt(a)));
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
    
    private List<ShipSpace.Ship> GenerateSingleOrderShips(ref List<OrderSpace.Order> orders, List<Point> autoparks)
    {
        var ordersToGenerate = orders
            .Where(o => o.Payloads.All(p => 
                p.PayloadType == PayloadTypes.Bulk
                || p.PayloadType == PayloadTypes.Liquid
            )
        );

        if (!ordersToGenerate.Any())
            return new List<ShipSpace.Ship>();
        
        orders = orders.ExceptBy(ordersToGenerate, o => o).ToList();

        var routes = new List<List<Point>>();
        var ships = new List<ShipSpace.Ship>();
        
        foreach (var o in ordersToGenerate)
        {
            routes.Add(new List<Point>
            {
                new Point(o.LoadAddress, PointType.Order, o.Id),
                new Point(o.DeliverAddress, PointType.Order, o.Id)
            });
        }
        
        for (int i = 0; i < routes.Count; i++)
        {
            var min1 = MinAutoparks(autoparks, routes[i].First());
            if (min1 == Invalid)
                throw new ApiException("There is no autoparks", ApiException.BadRequest);
            
            var min2 = MinAutoparks(autoparks, routes[i].Last());

            var stops = new List<Stop>()
            {
                new Stop
                {
                    Number = 1,
                    OrderId = routes[i][1].EntityId,
                    Time = null
                },

                new Stop
                {
                    Number = 2,
                    OrderId = routes[i][1].EntityId,
                    Time = null
                }
            };

            ships.Add(new ShipSpace.Ship()
            {
                AutoparkFinishId = (int)min2.Point.EntityId,
                AutoparkStartId = (int)min1.Point.EntityId,
                Stops = stops,
                Start = null,
                Finish = null
            });
        }

        return ships;        
    }

    private Graph GenerateGraph(double [,] distanceMap,  int nOrders)
    {
        var g = new Graph();

        for (int i = 0; i < nOrders; i++) 
            g.AddVertex($"_{i}");

        for (int i = 0; i < nOrders; i++)
            g.AddVertex($"_{i + nOrders}");
            
        for (int i = 0; i < 2 * nOrders; i++)
        {
            for (int j = 0; j < 2 * nOrders; j++)
            {
                if (double.IsFinite(distanceMap[i,j]))
                {
                    g.AddEdge($"_{i}", $"_{j}", distanceMap[i, j]);
                }
            }
        }

        return g;
    }
    
    public async Task<List<ShipSpace.Ship>> BuildRoutesAsync(CancellationToken cancellationToken)
    {
        var orders = (await _orderRepository.GetAcceptedOrdersWithPayloadsAsync(cancellationToken)).ToList();
        var autoparks = await _autoparkRepository.GetAutoparksWithAddressesVehicleAsync(cancellationToken);

        if (autoparks.Count() == 0)
            throw new ApiException("There is no autopark", ApiException.BadRequest);
        
        var autoparkPoints = new List<Point>();
            
        foreach (var a in autoparks)
        {
            autoparkPoints.Add(new Point(a.Address, PointType.Autopark, a.Id));
        }
        
        var singleOrderShips = GenerateSingleOrderShips(ref orders, autoparkPoints); 
        
        int nOrders = orders.Count;
        int nAutoparks = autoparks.Count();

        var routes = new List<List<Point>>();
        var completedRoutes = new List<List<Point>>();
        var routeDistances = new List<double>();
        
        foreach (var o in orders)
        {
            routes.Add(new List<Point>
            {
                new Point(o.LoadAddress, PointType.Order, o.Id),
                new Point(o.DeliverAddress, PointType.Order, o.Id)
            });

            routeDistances.Add(CountDistance(o.LoadAddress, o.DeliverAddress));
        }
        
        
        
        while (routes.Count > 0)
        {
            SortListsByDistances(ref routeDistances, ref routes);

            var r = routes.First();
            
            var mins = new PointInfo[]
            {
                r.First().PointType == PointType.Order ? MinAutoparks(autoparkPoints, r.First()) : Invalid,
                r.First().PointType == PointType.Order ? MinFromOtherLast(routes, r.First()) : Invalid,
                r.Last().PointType == PointType.Order ? MinAutoparks(autoparkPoints, r.Last()) : Invalid,
                r.Last().PointType == PointType.Order ? MinFromOtherFirst(routes, r.Last()) : Invalid,
            };

            var trueMin = mins.MinBy(pi => pi.Distance)!;

            if (trueMin == Invalid)
                throw new Exception();

            if (trueMin.Point.PointType == PointType.Order)
            {
                if (trueMin.For == r.First())
                    r = routes[trueMin.MemberOf].Concat(r).ToList();
                else if (trueMin.For == r.Last())
                    r = r.Concat(routes[trueMin.MemberOf]).ToList();
                    
                routeDistances[0] = routeDistances[0] + routeDistances[trueMin.MemberOf];
            }
            else 
            {
                if (trueMin.For == r.First())
                    r.Insert(0, autoparkPoints[trueMin.MemberOf]);
                else 
                    r.Add(autoparkPoints[trueMin.MemberOf]);
                
                routeDistances[0] += CountDistance(autoparkPoints[trueMin.MemberOf].Address, trueMin.For.Address);
            }

            routes[0] = r;
            
            if (trueMin.Point.PointType == PointType.Order)
            {
                routes.RemoveAt(trueMin.MemberOf);
                routeDistances.RemoveAt(trueMin.MemberOf);
            }
            
            if (r.First().PointType == PointType.Autopark && r.Last().PointType == PointType.Autopark)
            {
                routes.RemoveAt(0);
                routeDistances.RemoveAt(0);
                completedRoutes.Add(r);
            }
        }

        var result = new List<ShipSpace.Ship>();
        
        
        foreach (var r in completedRoutes)
        {
            var stops = new List<Stop>();
            var ship = new ShipSpace.Ship
            {
                AutoparkStartId = (int)r.First().EntityId,
                AutoparkFinishId = (int)r.Last().EntityId
            };

            for (int i = 1; i < r.Count - 1; i++)
            {
                var stop = new Stop()
                {
                    Number = (short)i,
                    OrderId = r[i].EntityId,
                    Time = null,
                };

                stops.Add(stop);
            }

            ship.Stops = stops;
            
            result.Add(ship);
        }
        
        return result.Concat(singleOrderShips).ToList();        
    }
    
    private void SortListsByDistances(ref List<double> distances, ref List<List<Point>> routes)
    {
        for (int i = 0; i < routes.Count - 1; i++)
        {
            int iMin = i;
            for (int j = i; j < routes.Count; j++)
            {
                if (distances[j] < distances[iMin])
                    iMin = j;
            }

            (distances[iMin], distances[i]) = (distances[i], distances[iMin]);
            (routes[iMin], routes[i]) = (routes[i], routes[iMin]);
        }
    }
    
    private PointInfo MinAutoparks(List<Point> autoparks, Point point)
    {
        if (autoparks.Count == 0)
            return Invalid;
            
        Point pointmin = autoparks[0];
        var min = CountDistance(pointmin.Address, point.Address);
        int iMin = 0;
        
        for (int i = 1; i < autoparks.Count; i++)
        {
            var a = autoparks[i];
                
            var distance = CountDistance(a.Address, point.Address); 
            if (distance < min)
            {
                iMin = i;
                min = distance;
                pointmin = a;
            }
            
        }
        return new PointInfo(pointmin, point, iMin, min);
    }
    
    private PointInfo MinFromOtherLast(List<List<Point>> routes, Point point)
    {
        if (routes.Count < 2)
            return Invalid;
        
        Point pointmin = routes[1].Last();
        var min = CountDistance(pointmin.Address, point.Address);
        int iMin = 1;
        
        for (int i = 2; i < routes.Count; i++)
        {
            var r = routes[i];
                
            var distance = CountDistance(r.Last().Address, point.Address); 
            if (distance < min)
            {
                iMin = i;
                min = distance;
                pointmin = r.Last();
            }
            
        }
        return new PointInfo(pointmin, point, iMin, min);
    }
    
    private PointInfo MinFromOtherFirst(List<List<Point>> routes, Point point)
    {
        if (routes.Count < 2)
            return Invalid;
            
        Point pointmin = routes[1].First();
        var min = CountDistance(pointmin.Address, point.Address);
        int iMin = 1;
        
        for (int i = 2; i < routes.Count; i++)
        {
            var r = routes[i];
                
            var distance = CountDistance(r.First().Address, point.Address); 
            if (distance < min)
            {
                iMin = i;
                min = distance;
                pointmin = r.First();
            }
            
        }
        return new PointInfo(pointmin, point, iMin, min);
    }
    
    public enum ValueType
    {
        Weight,
        Volume
    }

    public class ListNode<T> where T: Stop
    {
        public T Stop { get; set; } = null!;
        public ListNode<T>? Next { get; set; }
    }

    public enum PointType
    {
        Order,
        Autopark
    }

    public record Point(Address Address, PointType PointType, long EntityId);

    public record PointInfo(Point Point, Point For, int MemberOf, double Distance);

    public static PointInfo Invalid = new PointInfo(null, null, 0, double.PositiveInfinity);
}


