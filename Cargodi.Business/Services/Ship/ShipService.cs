using AutoMapper;
using Cargodi.Business.Constants;
using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.Business.DTOs.Ship.Stop;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Ship;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Ship;
using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Interfaces.Ship;
using Cargodi.DataAccess.Interfaces.Staff;

namespace Cargodi.Business.Services.Ship;

public class ShipService : IShipService
{
    private readonly ICarRepository _carRepository;
    private readonly ITrailerRepository _trailerRepository;
    private readonly IShipRepository _shipRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly IShipGeneratingService _shipGeneratingService;
    private readonly IMapper _mapper;
    
    
    public ShipService(ICarRepository carRepository, ITrailerRepository trailerRepository, 
        IShipRepository shipRepository, IDriverRepository driverRepository, 
        IShipGeneratingService shipGeneratingService, IMapper mapper)
    {
        _carRepository = carRepository;
        _trailerRepository = trailerRepository;
        _shipRepository = shipRepository;
        _driverRepository = driverRepository;
        _shipGeneratingService = shipGeneratingService;
        _mapper = mapper;
    }

    public Task<IEnumerable<GetShipDto>> GenerateAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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

    public async Task<GetShipDto> MarkAsync(int shipId, CancellationToken cancellationToken)
    {
        var ship = await _shipRepository.GetByIdAsync(shipId, cancellationToken)
            ?? throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);
            
        if (ship.Start == null)
        {
            ship.Start = DateTime.UtcNow;
        }
        else if (ship.Finish != null)
        {
            throw new ApiException("Ship is closed", ApiException.BadRequest);
        }
        
        ship.Finish = DateTime.UtcNow;

        var shipRes = _shipRepository.Update(ship);

        var car = await _carRepository.GetByIdAsync(ship.CarId, cancellationToken);
        car!.ActualAutoparkId = ship.AutoparkFinishId;
        _carRepository.Update(car);
        
        if (ship.Trailer != null)
        {
            var trailer = await _trailerRepository.GetByIdAsync(ship.TrailerId, cancellationToken);
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

        var orderIds = stopDtos.Select(stop => stop.OrderId).ToList();
        
        for (int i = 0; i < orderIds.Count; i++)
        {
            
        }
    }
}