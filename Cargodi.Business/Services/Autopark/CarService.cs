using AutoMapper;
using Cargodi.Business.Constants;
using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Interfaces.Autopark;

namespace Cargodi.Business.Services.Autopark;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IAutoparkRepository _autoparkRepository;
    
    private readonly IMapper _mapper;
    
    public CarService
        (ICarRepository carRepository, IAutoparkRepository autoparkRepository,
        IMapper mapper)
    {
        _carRepository = carRepository;
        _autoparkRepository = autoparkRepository;
        _mapper = mapper;
    }
    
    public async Task<GetCarDto> CreateAsync(UpdateCarDto carDto, CancellationToken cancellationToken)
    {
        await ValidateRequest(carDto, cancellationToken);
        
        if (await _carRepository.DoesItExistAsync(carDto.LicenseNumber!, cancellationToken))
        {
            throw new ApiException("license number is reserved", ApiException.BadRequest);
        }
                        
        var car = _mapper.Map<Car>(carDto);

        car.ActualAutoparkId = car.AutoparkId;
        
        car = await _carRepository.CreateAsync(car, cancellationToken);
        await _carRepository.SaveChangesAsync(cancellationToken);

        car = await _carRepository.GetCarFullInfoByIdAsync(car.Id, cancellationToken);
        
        return _mapper.Map<GetCarDto>(car);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new ApiException("Car not found", ApiException.NotFound);

        _carRepository.Delete(car);
        await _carRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<GetCarDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var cars = await _carRepository.GetAllCarsFullInfoAsync(cancellationToken);

        return _mapper.Map<IEnumerable<GetCarDto>>(cars);
    }

    public async Task<GetCarDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetCarFullInfoByIdAsync(id, cancellationToken)
            ?? throw new ApiException("Car not found", ApiException.NotFound);

        return _mapper.Map<GetCarDto>(car);
    }

    public async Task<GetCarDto> UpdateAsync(int id, UpdateCarDto carDto, CancellationToken cancellationToken)
    {
        await ValidateRequest(carDto, cancellationToken);
        
        var carEnt = await _carRepository.GetByIdAsync(id, cancellationToken) ??
            throw new ApiException("Car not found", ApiException.NotFound);

        var carEntityWithLicenseNumber
            = await _carRepository.GetByLicenseNumberAsync(carDto.LicenseNumber, cancellationToken);
            
        if (carEntityWithLicenseNumber != null || !carEntityWithLicenseNumber.LicenseNumber.Equals(carDto.LicenseNumber))
        {
            throw new ApiException("Car with such license number exists", ApiException.BadRequest);
        }
    
        
        var car = _mapper.Map<Car>(carDto);
        car.Id = id;

        car = _carRepository.Update(car);
        await _carRepository.SaveChangesAsync(cancellationToken);

        car = await _carRepository.GetCarFullInfoByIdAsync(id, cancellationToken);
        
        return _mapper.Map<GetCarDto>(car);
    }
    
    private async Task ValidateRequest(UpdateCarDto updateCarDto, CancellationToken cancellationToken)
    {
        if (!(await _autoparkRepository.DoesItExistAsync(updateCarDto.ActualAutoparkId, cancellationToken)))
        {
            throw new ApiException("autopark doesn't exist", ApiException.BadRequest);
        }
        
        if (!(await _autoparkRepository.DoesItExistAsync(updateCarDto.AutoparkId, cancellationToken)))
        {
            throw new ApiException("autopark doesn't exist", ApiException.BadRequest);
        }

        if (!(CarTypes.PassengerCar.Id == updateCarDto.CarTypeId
            || CarTypes.Van.Id == updateCarDto.CarTypeId
            || CarTypes.Truck.Id == updateCarDto.CarTypeId
        ))
            throw new ApiException("invalid car type", ApiException.BadRequest);
    }

}