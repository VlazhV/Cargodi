using AutoMapper;
using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Interfaces.Autopark;

namespace Cargodi.Business.Services.Autopark;

public class CarService : ICarService
{
	private readonly ICarRepository _carRepository;
	private readonly IMapper _mapper;
	
	public CarService
		(ICarRepository carRepository,
		IMapper mapper)
	{
		_carRepository = carRepository;
		_mapper = mapper;
	}
	
	public async Task<GetCarDto> CreateAsync(UpdateCarDto carDto, CancellationToken cancellationToken)
	{
		if (await _carRepository.DoesItExistAsync(carDto.LicenseNumber!, cancellationToken))
		{
			throw new ApiException("license number is reserved", ApiException.BadRequest);
		}
						
		var car = _mapper.Map<Car>(carDto);

		car.ActualAutoparkId = car.AutoparkId;
		
		car = await _carRepository.CreateAsync(car, cancellationToken);
		await _carRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetCarDto>(car);
	}

	public async Task DeleteAsync(int id, CancellationToken cancellationToken)
	{
		var car = await _carRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException("Car not found", ApiException.NotFound);

		_carRepository.Delete(car);
		await _carRepository.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<GetCarAutoparkDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var cars = await _carRepository.GetAllAsync(cancellationToken);

		return _mapper.Map<IEnumerable<GetCarAutoparkDto>>(cars);
	}

	public async Task<GetCarAutoparkDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var car = await _carRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException("Car not found", ApiException.NotFound);

		return _mapper.Map<GetCarAutoparkDto>(car);
	}

	public async Task<GetCarDto> UpdateAsync(int id, UpdateCarDto carDto, CancellationToken cancellationToken)
	{
		if (! await _carRepository.DoesItExistAsync(id, cancellationToken))
		{
			throw new ApiException("Car not found", ApiException.NotFound);
		}
			
		if (await _carRepository.DoesItExistAsync(carDto.LicenseNumber!, cancellationToken))
		{
			throw new ApiException("License number is reserved", ApiException.BadRequest);
		}			
		
		var car = _mapper.Map<Car>(carDto);
		car.Id = id;

		car = _carRepository.Update(car);
		await _carRepository.SaveChangesAsync(cancellationToken);
		
		return _mapper.Map<GetCarDto>(car);
	}

}