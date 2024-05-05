using System.Security.Claims;
using AutoMapper;
using Cargodi.Business.Constants;
using Cargodi.Business.DTOs.Order.Order;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Order;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Interfaces.Order;
using Cargodi.DataAccess.Interfaces.Staff;

namespace Cargodi.Business.Services.Order;

public class OrderService : IOrderService
{
	private readonly IPayloadRepository _payloadRepository;
	private readonly IOrderRepository _orderRepository;
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	
	public OrderService
		(IPayloadRepository payloadRepository,
		IOrderRepository orderRepository,
		IUserRepository userRepository,
		IMapper mapper)
	{
		_payloadRepository = payloadRepository;
		_orderRepository = orderRepository;
		_userRepository = userRepository;
		_mapper = mapper;
	}
	
	public async Task<GetOrderDto> CreateAsync(long? customerId, ClaimsPrincipal user, UpdateOrderPayloadsDto orderDto, CancellationToken cancellationToken)
	{
		long clientId;

		if (customerId.HasValue)
		{
			var role = user.FindFirst(ClaimTypes.Role)!.Value;
		
			if (!(role == Roles.Admin || role == Roles.Manager))
			{
				throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
			}
			
			clientId = customerId.Value;
		}
		else
		{
			clientId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		}

		if (! await _userRepository.DoesItExistAsync(clientId, cancellationToken))
		{
			throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);
		}

		var order = _mapper.Map<DataAccess.Entities.Order.Order>(orderDto);
		order.ClientId = clientId;
		order.OrderStatusId = OrderStatuses.Processing.Id;
		order.Time = DateTime.UtcNow;

		order = await _orderRepository.CreateAsync(order, cancellationToken);
		order.OrderStatus = OrderStatuses.Processing;
		await _orderRepository.SaveChangesAsync(cancellationToken);
		
		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task DeleteAsync(long id, ClaimsPrincipal user, CancellationToken cancellationToken)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;

		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
		
		if (order is null)
		{
			throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);	
		}

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}
			

		_orderRepository.Delete(order);
		await _orderRepository.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<GetOrderInfoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var orders = await _orderRepository.GetAllAsync(cancellationToken);

		return _mapper.Map<IEnumerable<GetOrderInfoDto>>(orders);
	}

	public async Task<GetOrderInfoDto> GetByIdAsync(long id, ClaimsPrincipal user, CancellationToken cancellationToken)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
		
		if (order is null)
		{
			throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);
		}			
			
		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		return _mapper.Map<GetOrderInfoDto>(order);
	}

	public async Task<GetOrderDto> SetStatusAsync(long id, string status, ClaimsPrincipal user, CancellationToken cancellationToken)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;

		if (!(role == Roles.Admin || role == Roles.Manager))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		var order = await _orderRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);

		order = await _orderRepository.SetStatusAsync(order, status.ToLower(), cancellationToken)
			?? throw new ApiException(Messages.IncorrectOrderStatus, ApiException.BadRequest);

		if (status.Equals(OrderStatuses.Accepted.Name))
		{
			order.AcceptTime = DateTime.UtcNow;

			order = _orderRepository.Update(order);
		}

		await _orderRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task<GetOrderDto> UpdateAsync(long id, ClaimsPrincipal user, UpdateOrderDto orderDto, CancellationToken cancellationToken)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var order = await _orderRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		order = _mapper.Map(orderDto, order);
		order = _orderRepository.Update(order);
		await _orderRepository.SaveChangesAsync(cancellationToken);

		return _mapper.Map<GetOrderDto>(order);
	}

	public async Task<GetOrderInfoDto> UpdatePayloadListAsync(long id, ClaimsPrincipal user, IEnumerable<UpdatePayloadDto> payloadDtos, CancellationToken cancellationToken)
	{
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

		var order = await _orderRepository.GetByIdAsync(id, cancellationToken)
			?? throw new ApiException(Messages.OrderIsNotFound, ApiException.NotFound);

		if (!(role == Roles.Admin || role == Roles.Manager || userId == order.ClientId))
		{
			throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
		}

		_orderRepository.ClearPayloadList(order);
		var payloads = _mapper.Map<IEnumerable<Payload>>(payloadDtos);
		
		foreach(var payload in payloads)
		{
			payload.OrderId = id;
		}

		await _payloadRepository.CreateManyAsync(payloads, cancellationToken);		
		await _orderRepository.SaveChangesAsync(cancellationToken);
		
		order = await _orderRepository.GetByIdAsync(id, cancellationToken);

		return _mapper.Map<GetOrderInfoDto>(order);
	}
}