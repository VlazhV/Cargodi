using System.Security.Claims;
using AutoMapper;
using Cargodi.Business.Constants;
using Cargodi.Business.DTOs.Order.Order;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Order;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Interfaces.Common;
using Cargodi.DataAccess.Interfaces.Order;
using Cargodi.DataAccess.Interfaces.Staff;

namespace Cargodi.Business.Services.Order;

public class OrderService : IOrderService
{
    private readonly IPayloadRepository _payloadRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IOperatorRepository _operatorRepository;
    private readonly IMapper _mapper;
    
    public OrderService
        (IPayloadRepository payloadRepository,
        IOrderRepository orderRepository,
        IClientRepository userRepository,
        IAddressRepository addressRepository,
        IOperatorRepository operatorRepository,
        IMapper mapper)
    {
        _payloadRepository = payloadRepository;
        _orderRepository = orderRepository;
        _clientRepository = userRepository;
        _addressRepository = addressRepository;
        _operatorRepository = operatorRepository;
        _mapper = mapper;
    }
    
    public async Task<GetOrderInfoDto> CreateAsync(long? customerId, ClaimsPrincipal user, UpdateOrderPayloadsDto orderDto, CancellationToken cancellationToken)
    {
        long clientId = 0;

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
            var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var client = await _clientRepository.GetClientByUserIdAsync(userId, cancellationToken);

            if (client == null)
                throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);
             
            clientId = client.Id;
        }

        var payloads = _mapper.Map<List<Payload>>(orderDto.Payloads);
        
        var order = _mapper.Map<DataAccess.Entities.Order.Order>(orderDto);
        order.ClientId = clientId;
        order.OrderStatusId = OrderStatuses.Processing.Id;
        order.Time = DateTime.UtcNow;

        order.Payloads = null;

        order.DeliverAddressId = await ProcessAddressAsync(order.DeliverAddress, cancellationToken);
        order.DeliverAddress = null;
        
        order.LoadAddressId = await ProcessAddressAsync(order.LoadAddress, cancellationToken);
        order.LoadAddress = null;
        
        order = await _orderRepository.CreateAsync(order, cancellationToken);
        await _orderRepository.SaveChangesAsync(cancellationToken);

        await CreatePayloadsWithSaveAsync(order.Id, payloads, cancellationToken);
        
       
        order = await _orderRepository.GetByIdAsync(order.Id, cancellationToken);
        return _mapper.Map<GetOrderInfoDto>(order);
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

        if (!(role == Roles.Admin || role == Roles.Manager || userId == order.Client.UserId))
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

    public async Task<IEnumerable<GetOrderInfoDto>> GetAllOfClientAsync(long userId, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllOfClientAsync(userId, cancellationToken);

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
            
        if (!(role == Roles.Admin || role == Roles.Manager || userId == order.Client.UserId))
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

        var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var @operator = await _operatorRepository.GetOperatorByUserIdAsync(userId, cancellationToken)
            ?? throw new ApiException(Messages.UserIsNotFound, ApiException.NotFound);

        if (status.Equals(OrderStatuses.Accepted.Name))
        {
            order.AcceptTime = DateTime.UtcNow;
            order.OperatorId = @operator.Id;
            order = _orderRepository.Update(order);
        }
        else if (status.Equals(OrderStatuses.Declined.Name))
        {
            order.AcceptTime = null;
            order.OperatorId = @operator.Id;
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
        
        if (!(role == Roles.Admin || role == Roles.Manager || userId == order.Client.UserId))
        {
            throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
        }
        
        if (order.OrderStatusId != OrderStatuses.Processing.Id)
        {
            throw new ApiException("Order is processed", ApiException.BadRequest);
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

        if (!(role == Roles.Admin || role == Roles.Manager || userId == order.Client.UserId))
        {
            throw new ApiException(Messages.NoPermission, ApiException.Forbidden);
        }
        
        

        _orderRepository.ClearPayloadList(order);
        var payloads = _mapper.Map<List<Payload>>(payloadDtos);
        
        await CreatePayloadsWithSaveAsync(id, payloads, cancellationToken);		
        
        order = await _orderRepository.GetByIdAsync(id, cancellationToken);

        return _mapper.Map<GetOrderInfoDto>(order);
    }
    
    private async Task<long> ProcessAddressAsync(Address address, CancellationToken cancellationToken)
    {
        var addressEntity = await _addressRepository.GetIfExistsAsync(address, cancellationToken);
        
        if (addressEntity == null)
        {
            addressEntity = await _addressRepository.CreateAsync(address, cancellationToken);
            await _addressRepository.SaveChangesAsync(cancellationToken);
        }

        return addressEntity.Id;
    }

    private async Task CreatePayloadsWithSaveAsync(long orderId, List<Payload> payloads, CancellationToken cancellationToken)
    {
        bool valid = payloads.All(p =>
            p.PayloadTypeId == PayloadTypes.Bulk.Id ||
            p.PayloadTypeId == PayloadTypes.Liquid.Id ||
            p.PayloadTypeId == PayloadTypes.Item.Id
            );
            
        if (!valid)
        {
            throw new ApiException("Invalid Paylod Types", ApiException.BadRequest);
        }

        payloads.ForEach(p => {
            p.PayloadTypeId = p.PayloadType.Id;
            p.PayloadType = null;
            p.OrderId = orderId;
        });

        await _payloadRepository.CreateManyAsync(payloads, cancellationToken);
        await _payloadRepository.SaveChangesAsync(cancellationToken);
    }
}