using AutoMapper;
using Cargodi.Business.DTOs.Autopark.Autopark;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Interfaces.Autopark;

namespace Cargodi.Business.Services.Autopark;

public class AutoparkService : IAutoparkService
{
	private readonly IAutoparkRepository _autoparkRepository;
	private readonly IMapper _mapper;
	
	public AutoparkService(IAutoparkRepository autoparkRepository, IMapper mapper)
	{
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
		var autoparks = await _autoparkRepository.GetAllAsync(cancellationToken);

		return autoparks.Select(autopark => _mapper.Map<GetAutoparkVehicleDto>(autopark));
	}

	public async Task<GetAutoparkVehicleDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var autopark = await _autoparkRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException("Autopark not found", ApiException.NotFound);

		return _mapper.Map<GetAutoparkVehicleDto>(autopark);
	}

	public async Task<GetAutoparkDto> UpdateAsync(int id, UpdateAutoparkDto autoparkDto, CancellationToken cancellationToken)
	{
		if (! await _autoparkRepository.DoesItExistAsync(id, cancellationToken))
			throw new ApiException("Autopark not found", ApiException.NotFound);
		
		var autopark = _mapper.Map<DataAccess.Entities.Autopark.Autopark>(autoparkDto);
		autopark.Id = id;
		
		autopark = _autoparkRepository.Update(autopark);
		await _autoparkRepository.SaveChangesAsync(cancellationToken);
		
		return _mapper.Map<GetAutoparkDto>(autopark);
	}

}