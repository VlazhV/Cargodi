using AutoMapper;
using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.DTOs.Autopark.Trailer;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Repositories.Autopark;

namespace Cargodi.Business.Services.Autopark;

public class TrailerService : ITrailerService
{
	private readonly ITrailerRepository _trailerRepository;
	private readonly IAutoparkRepository _autoparkRepository;
	private readonly IMapper _mapper;
	
	public TrailerService
		(ITrailerRepository trailerRepository, 
		IAutoparkRepository autoparkRepository,
	IMapper mapper)
	{
		_trailerRepository = trailerRepository;
		_autoparkRepository = autoparkRepository;	
		_mapper = mapper;
	}
	
	public async Task<GetTrailerDto> CreateAsync(UpdateTrailerDto trailerDto, CancellationToken cancellationToken)
	{
		await ValidateRequest(trailerDto, cancellationToken);

		if (await _trailerRepository.DoesItExistAsync(trailerDto.LicenseNumber!, cancellationToken))
		{
			throw new ApiException("License number is reserved", ApiException.BadRequest);
		}

		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer = await _trailerRepository.CreateAsync(trailer, cancellationToken);
		await _trailerRepository.SaveChangesAsync(cancellationToken);

		trailer = await _trailerRepository.GetTrailerFullInfoByIdAsync(trailer.Id, cancellationToken);

		return _mapper.Map<GetTrailerDto>(trailer);
	}

	public async Task DeleteAsync(int id, CancellationToken cancellationToken)
	{
		var trailer = await _trailerRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException("Trailer not found", ApiException.NotFound);

		_trailerRepository.Delete(trailer);
		await _trailerRepository.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<GetTrailerDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var trailers = await _trailerRepository.GetAllTrailersFullInfoAsync(cancellationToken);

		return _mapper.Map<IEnumerable<GetTrailerDto>>(trailers);
	}

	public async Task<GetTrailerDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var trailer = await _trailerRepository.GetTrailerFullInfoByIdAsync(id, cancellationToken)
			?? throw new ApiException("Trailer not found", ApiException.NotFound);

		return _mapper.Map<GetTrailerDto>(trailer);
	}

	public async Task<GetTrailerDto> UpdateAsync(int id, UpdateTrailerDto trailerDto, CancellationToken cancellationToken)
	{
		await ValidateRequest(trailerDto, cancellationToken);

		if (! await _trailerRepository.DoesItExistAsync(id, cancellationToken))
		{
			throw new ApiException("Trailer not found", ApiException.NotFound);
		}

		var carEntityWithLicenseNumber
		= await _trailerRepository.GetByLicenseNumberAsync(trailerDto.LicenseNumber, cancellationToken);

		if (carEntityWithLicenseNumber != null || !carEntityWithLicenseNumber.LicenseNumber.Equals(trailerDto.LicenseNumber))
		{
			throw new ApiException("Trailer with such license number exists", ApiException.BadRequest);
		}

		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer.Id = id;

		trailer = _trailerRepository.Update(trailer);
		await _trailerRepository.SaveChangesAsync(cancellationToken);

		trailer = await _trailerRepository.GetTrailerFullInfoByIdAsync(id, cancellationToken);
		
		return _mapper.Map<GetTrailerDto>(trailer);
	}

	private async Task ValidateRequest(UpdateTrailerDto trailerDto, CancellationToken cancellationToken)
	{
		if (!(await _autoparkRepository.DoesItExistAsync(trailerDto.ActualAutoparkId, cancellationToken)))
		{
			throw new ApiException("autopark doesn't exist", ApiException.BadRequest);
		}

		if (!(await _autoparkRepository.DoesItExistAsync(trailerDto.AutoparkId, cancellationToken)))
		{
			throw new ApiException("autopark doesn't exist", ApiException.BadRequest);
		}

		if (!(TrailerTypes.Cistern.Id == trailerDto.TrailerTypeId
			|| TrailerTypes.Bulker.Id == trailerDto.TrailerTypeId
			|| TrailerTypes.VanTrailer.Id == trailerDto.TrailerTypeId
		))
			throw new ApiException("invalid trailer type", ApiException.BadRequest);
	}

}