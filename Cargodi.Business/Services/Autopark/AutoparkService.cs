using AutoMapper;
using Cargodi.Business.Constants;
using Cargodi.Business.DTOs.Autopark.Autopark;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Interfaces.Common;

namespace Cargodi.Business.Services.Autopark;

public class AutoparkService : IAutoparkService
{
    private readonly IAutoparkRepository _autoparkRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;
    
    public AutoparkService(IAutoparkRepository autoparkRepository, IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _autoparkRepository = autoparkRepository;
        _mapper = mapper;
    }
    
    public async Task<GetAutoparkDto> CreateAsync(UpdateAutoparkDto autoparkDto, CancellationToken cancellationToken)
    {
        var autopark = _mapper.Map<DataAccess.Entities.Autopark.Autopark>(autoparkDto);
        
        autopark = await _autoparkRepository.CreateAsync(autopark, cancellationToken);
        await _autoparkRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetAutoparkDto>(autopark);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var autopark = await _autoparkRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new ApiException("Autopark not found", ApiException.NotFound);

        _autoparkRepository.Delete(autopark);
        await _autoparkRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<GetAutoparkVehicleDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var autoparks = await _autoparkRepository.GetAutoparksWithAddressesVehicleAsync(cancellationToken);

        return autoparks.Select(autopark => _mapper.Map<GetAutoparkVehicleDto>(autopark));
    }

    public async Task<GetAutoparkVehicleDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var autopark = await _autoparkRepository.GetAutoparkWithAddressesVehicleByIdAsync(id, cancellationToken)
            ?? throw new ApiException("Autopark not found", ApiException.NotFound);

        return _mapper.Map<GetAutoparkVehicleDto>(autopark);
    }

    public async Task<GetAutoparkDto> UpdateAsync(int id, UpdateAutoparkDto autoparkDto, CancellationToken cancellationToken)
    {
        var autoparkEntity = await _autoparkRepository.GetAutoparkWithAddressesVehicleByIdAsync(id, cancellationToken)
            ?? throw new ApiException("Autopark not found", ApiException.NotFound);

        var addressEntity = autoparkEntity.Address;

        var autopark = _mapper.Map<DataAccess.Entities.Autopark.Autopark>(autoparkDto);
        autopark.Id = id;
        
        if (!autopark.Address.Equals(addressEntity))        
        {
            var addressEntry = await _addressRepository.CreateAsync(autopark.Address, cancellationToken);
            autoparkEntity.AddressId = addressEntry.Id;
            autoparkEntity.Address = addressEntry;
        }
        autoparkEntity.Capacity = autopark.Capacity;
        
        //autopark = _autoparkRepository.Update(autopark);
        await _autoparkRepository.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<GetAutoparkDto>(autopark);
    }

}