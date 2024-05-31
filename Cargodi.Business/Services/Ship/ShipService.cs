using System.Security.Claims;
using AutoMapper;
using Cargodi.Business.Constants;
using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.Business.DTOs.Ship.Stop;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Ship;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Autopark;
using ShipSpace = Cargodi.DataAccess.Entities.Ship;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Interfaces.Ship;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Collections.Immutable;
using Cargodi.DataAccess.Interfaces.Order;

namespace Cargodi.Business.Services.Ship;

public class ShipService : IShipService
{
    private readonly ICarRepository _carRepository;
    private readonly ITrailerRepository _trailerRepository;
    private readonly IShipRepository _shipRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly IShipGeneratingService _shipGeneratingService;
    private readonly IOperatorRepository _operatorRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    
    
    public ShipService(ICarRepository carRepository, ITrailerRepository trailerRepository, 
        IShipRepository shipRepository, IDriverRepository driverRepository, 
        IShipGeneratingService shipGeneratingService, IOperatorRepository operatorRepository,
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _carRepository = carRepository;
        _trailerRepository = trailerRepository;
        _shipRepository = shipRepository;
        _driverRepository = driverRepository;
        _shipGeneratingService = shipGeneratingService;
        _operatorRepository = operatorRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetShipDto>> GenerateAsync(ClaimsPrincipal user, int driversCount, CancellationToken cancellationToken)
    {
        var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var @operator = await _operatorRepository.GetOperatorByUserIdAsync(userId, cancellationToken)
            ?? throw new ApiException(Messages.UserIsNotFound, ApiException.Forbidden);
            
        var ships = await _shipGeneratingService.BuildRoutesAsync(cancellationToken);
        var shipsToCreate = new List<ShipSpace.Ship>();
        
        foreach (var ship in ships)
        {
            Car car;
            Trailer? trailer = null;
            IEnumerable<Driver> drivers; 
            
            var carriers = await _shipGeneratingService.SelectVehicleAsync(ship, cancellationToken);
            if (!carriers.Any())
                throw new ApiException("there is no suitable cars", ApiException.BadRequest);
            
            if (carriers.First().GetType() == typeof(Car))
            {
                car = (Car)carriers.First();
            }
            else 
            {
                trailer = (Trailer)carriers.First();
                car = (await _carRepository.GetCarsOfTypeAsync(CarTypes.Truck, cancellationToken)).First();
            }

            drivers = (await _shipGeneratingService.SelectDriversAsync(car, cancellationToken)).Take(driversCount);

            if (!drivers.Any())
                throw new ApiException("there is no suitable drivers", ApiException.BadRequest);


            ship.OperatorId = @operator.Id;
            ship.Drivers = drivers.ToImmutableList();
            ship.CarId = car.Id;
            ship.TrailerId = trailer?.Id;
            ship.Start = null;
            ship.Finish = null;

            shipsToCreate.Add(ship);
        }

        await _shipRepository.CreateManyAsync(shipsToCreate, cancellationToken);
        _orderRepository.UpdateAcceptedOrdersToCompleting();
        await _shipRepository.SaveChangesAsync(cancellationToken);

        var shipsReturn = await _shipRepository.GetAllShipsFullInfoAsync(cancellationToken);

        return _mapper.Map<IEnumerable<GetShipDto>>(shipsReturn);
    }

    public async Task<IEnumerable<GetShipDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var ships = await _shipRepository.GetAllShipsFullInfoAsync(cancellationToken);

        return _mapper.Map<IEnumerable<GetShipDto>>(ships);
    }

    public async Task<GetShipDto> GetByIdAsync(int shipId, CancellationToken cancellationToken)
    {
        var ship = await _shipRepository.GetShipFullInfoByIdAsync(shipId, cancellationToken);
        
        if (ship == null)
            throw new ApiException(Messages.ShipIsNotFound, ApiException.NotFound);

        return _mapper.Map<GetShipDto>(ship);
    }

    public async Task<GetShipDto> MarkAsync(int shipId, ClaimsPrincipal user, CancellationToken cancellationToken)
    {    
        var ship = await _shipRepository.GetShipFullInfoByIdAsync(shipId, cancellationToken)
            ?? throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);

        var driverUserIds = ship.Drivers.Select(x => x.UserId);
        
        if (user.IsInRole(Roles.Driver))
        {
            var driverId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (!driverUserIds.Contains(driverId))
            {
                throw new ApiException("you have no rights", ApiException.Forbidden);
            }
        }
        
            
        if (ship.Start == null)
        {
            ship.Start = DateTime.UtcNow;            
            _shipRepository.Update(ship);
            await _shipRepository.SaveChangesAsync(cancellationToken);
            ship = await _shipRepository.GetShipFullInfoByIdAsync(shipId, cancellationToken);

            return _mapper.Map<GetShipDto>(ship);
        }
        else if (ship.Finish != null)
        {
            throw new ApiException("Ship is closed", ApiException.BadRequest);
        }
        
        ship.Finish = DateTime.UtcNow;

        var shipRes = _shipRepository.Update(ship);

        var orders = await _orderRepository.GetAllOfShipAsync(ship, cancellationToken);
        orders.ForEach(o => o.OrderStatusId = OrderStatuses.Completed.Id);
        _orderRepository.UpdateRange(orders);
        
        var car = await _carRepository.GetByIdAsync(ship.CarId, cancellationToken);
        car!.ActualAutoparkId = ship.AutoparkFinishId;
        _carRepository.Update(car);
        
        if (ship.Trailer != null)
        {
            var trailer = await _trailerRepository.GetByIdAsync(ship.TrailerId!.Value, cancellationToken);
            trailer!.ActualAutoparkId = ship.AutoparkFinishId;
            _trailerRepository.Update(trailer);
        }
        
        foreach(var d in ship.Drivers)
        {
            var driver =  await _driverRepository.GetByIdAsync(d.Id, cancellationToken);
            driver!.ActualAutoparkId = ship.AutoparkFinishId;
            _driverRepository.Update(driver);
        }

        await _shipRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GetShipDto>(shipRes);
    }

    public async Task<GetShipDto> UpdateAsync(int shipId, UpdateShipDto shipDto, CancellationToken cancellationToken)
    {
        var ship = await _shipRepository.GetShipFullInfoByIdAsync(shipId, cancellationToken);

        if (ship == null)
            throw new ApiException(Messages.ShipIsNotFound, ApiException.NotFound);

        var shipRequest = _mapper.Map<DataAccess.Entities.Ship.Ship>(shipDto);

        await ValidateVehicleAsync(shipRequest, cancellationToken);
        var car = await _carRepository.GetByIdAsync(shipDto.CarId, cancellationToken);
        await ValidateDrivers(shipDto.DriverIds, car!, cancellationToken);

        var stopsRequest = shipDto.Stops;
        await ValidateStops(stopsRequest, cancellationToken);

        // update vehicle
        ship.CarId = shipRequest.CarId;
        ship.TrailerId = shipRequest.TrailerId;

        // update drivers
        ship.Drivers = (await _driverRepository.GetDriversByIdsAsync(shipDto.DriverIds, cancellationToken)).ToList();

        // update stops

        _shipRepository.Update(ship);
        return _mapper.Map<GetShipDto>(ship);
        
    }
    
    private async Task ValidateVehicleAsync(DataAccess.Entities.Ship.Ship ship, CancellationToken cancellationToken)
    {
        var carriers = await _shipGeneratingService.SelectVehicleAsync(ship, cancellationToken);
        var trailers = carriers.Where(c => c.GetType() == typeof(Trailer)).Select(c => (Trailer)c);
        var cars = carriers.Where(c => c.GetType() == typeof(Car)).Select(c => (Car)c);
        
        if (ship.TrailerId != null)
        {            
            if (!trailers.Any(t => t.Id == ship.TrailerId))
                throw new ApiException("Invalid trailer", ApiException.BadRequest);

            var carRequest = await _carRepository.GetByIdAsync(ship.CarId, cancellationToken);

            if (carRequest == null)
                throw new ApiException("car is not found", ApiException.BadRequest);

            if (carRequest.CarTypeId != CarTypes.Truck.Id)
                throw new ApiException("Truck is needed for trailers", ApiException.BadRequest);
        }
        else 
        {
            if(!cars.Any(c => c.Id == ship.CarId))
                throw new ApiException("Invalid car", ApiException.BadRequest);
        }
    }
    
    private async Task ValidateDrivers(List<int> driverIds, Car car, CancellationToken cancellationToken)
    {
        if (!driverIds.Any())
        {
            throw new ApiException("ship cannot be without drivers", ApiException.BadRequest);
        }
        
        var validDriverIds = (await _shipGeneratingService.SelectDriversAsync(car, cancellationToken))
            .Select(x => x.Id);

        var intersection = validDriverIds.IntersectBy(driverIds, d => d);

        var invalidDriverIds = driverIds.ExceptBy(intersection, d => d);
        
        if (invalidDriverIds.Any())
        {
            throw new ApiException("Invalid drivers", ApiException.BadRequest);
        }
    }
    
    private async Task ValidateStops(IEnumerable<UpdateStopDto> stopDtos, CancellationToken cancellationToken)
    {
        if (!stopDtos.Any())
        {
            throw new ApiException("given zero stops", ApiException.BadRequest);
        }
        
        if (stopDtos.Count() % 2 == 0)
        {
            throw new ApiException("invalid route: the number of stops must be even", ApiException.BadRequest);
        }

        var orderIds = stopDtos.Select(stop => stop.OrderId)
            .OrderBy(id => id)
            .ToList();
        
        for (int i = 0; i < orderIds.Count; i += 2)
        {
            if (orderIds[i] != orderIds[i+1])
            {
                throw new ApiException("invalid route", ApiException.BadRequest);
            }
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var ship = await _shipRepository.GetByIdAsync(id, cancellationToken);
        
        if (ship == null)
        {
            throw new ApiException(Messages.ShipIsNotFound, ApiException.NotFound);
        }

        _shipRepository.Delete(ship);
        await _shipRepository.SaveChangesAsync(cancellationToken);        
    }

    public async Task<IEnumerable<GetShipDto>> GetAllOfDriverAsync(long userId, CancellationToken cancellationToken)
    {
        var ships = await _shipRepository.GetShipsFullInfoOfDriverAsync(userId, cancellationToken);

        return _mapper.Map<IEnumerable<GetShipDto>>(ships);
    }
}