using AutoMapper;
using Cargodi.Business.DTOs.Autopark.Trailer;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Interfaces.Autopark;

namespace Cargodi.Business.Services.Autopark;

public class TrailerService : ITrailerService
{
	private readonly ITrailerRepository _trailerRepository;
	private readonly IMapper _mapper;
	
	public TrailerService
		(ITrailerRepository trailerRepository,
		IMapper mapper)
	{
		_trailerRepository = trailerRepository;
		_mapper = mapper;
	}
	
	public async Task<GetTrailerDto> CreateAsync(UpdateTrailerDto trailerDto, CancellationToken cancellationToken)
	{
		if (await _trailerRepository.DoesItExistAsync(trailerDto.LicenseNumber!, cancellationToken))
		{
			throw new ApiException("License number is reserved", ApiException.BadRequest);
		}			
			
		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer = await _trailerRepository.CreateAsync(trailer, cancellationToken);
		await _trailerRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetTrailerDto>(trailer);
	}

	public async Task DeleteAsync(int id, CancellationToken cancellationToken)
	{
		var trailer = await _trailerRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException("Trailer not found", ApiException.NotFound);

		_trailerRepository.Delete(trailer);
		await _trailerRepository.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<GetTrailerAutoparkDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var trailers = await _trailerRepository.GetAllAsync(cancellationToken);

		return _mapper.Map<IEnumerable<GetTrailerAutoparkDto>>(trailers);
	}

	public async Task<GetTrailerAutoparkDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var trailer = await _trailerRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException("Trailer not found", ApiException.NotFound);

		return _mapper.Map<GetTrailerAutoparkDto>(trailer);
	}

	public async Task<GetTrailerDto> UpdateAsync(int id, UpdateTrailerDto trailerDto, CancellationToken cancellationToken)
	{
		if (! await _trailerRepository.DoesItExistAsync(id, cancellationToken))
		{
			throw new ApiException("Trailer not found", ApiException.NotFound);
		}
						
		if (await _trailerRepository.DoesItExistAsync(trailerDto.LicenseNumber!, cancellationToken))
		{
			throw new ApiException("License number is reserved", ApiException.BadRequest);	
		}			
		
		var trailer = _mapper.Map<Trailer>(trailerDto);
		trailer.Id = id;

		trailer = _trailerRepository.Update(trailer);
		await _trailerRepository.SaveChangesAsync(cancellationToken);
		
		return _mapper.Map<GetTrailerDto>(trailer);
	}

}