using AutoMapper;
using Cargodi.Business.Constants;
using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Ship;
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
	private readonly IMapper _mapper;
	
	
	public ShipService(ICarRepository carRepository, ITrailerRepository trailerRepository, 
		IShipRepository shipRepository, IDriverRepository driverRepository, IMapper mapper)
	{
		_carRepository = carRepository;
		_trailerRepository = trailerRepository;
		_shipRepository = shipRepository;
		_driverRepository = driverRepository;
		_mapper = mapper;
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

}