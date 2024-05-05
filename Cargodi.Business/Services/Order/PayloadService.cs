using System.Security.Claims;
using AutoMapper;
using Cargodi.Business.Constants;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Order;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Interfaces.Order;

namespace Cargodi.Business.Services.Order;

public class PayloadService : IPayloadService
{
	private readonly IPayloadRepository _payloadRepository;
	private readonly IMapper _mapper;
	
	public PayloadService(IPayloadRepository payloadRepository, IMapper mapper)
	{
		_payloadRepository = payloadRepository;
		_mapper = mapper;
	}	
	
	public async Task DeleteAsync(long id, ClaimsPrincipal user, CancellationToken cancellationToken)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var payload = await _payloadRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException(Messages.PayloadIsNotFound, ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		_payloadRepository.Delete(payload);
		await _payloadRepository.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<GetPayloadOrderDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var payloads = await _payloadRepository.GetAllAsync(cancellationToken);
		
		return _mapper.Map<IEnumerable<GetPayloadOrderDto>>(payloads);
	}

	public async Task<GetPayloadOrderDto> GetByIdAsync(long id, ClaimsPrincipal user, CancellationToken cancellationToken)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		
		var payload = await _payloadRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException(Messages.PayloadIsNotFound, ApiException.NotFound);
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		return _mapper.Map<GetPayloadOrderDto>(payload);
	}

	public async Task<GetPayloadDto> UpdateAsync(long id, ClaimsPrincipal user, UpdatePayloadDto payloadDto, CancellationToken cancellationToken)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var payload = await _payloadRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException(Messages.PayloadIsNotFound, ApiException.NotFound);
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == payload.Order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}
		
		var updatedPayload = _mapper.Map<Payload>(payloadDto);
		updatedPayload.Id = id;		
		updatedPayload.OrderId = payload.OrderId;

		payload = _payloadRepository.Update(updatedPayload);
		await _payloadRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetPayloadDto>(updatedPayload);
	}

}