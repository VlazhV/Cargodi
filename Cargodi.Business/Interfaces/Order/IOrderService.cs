using System.Security.Claims;
using Cargodi.Business.DTOs.Order.Order;
using Cargodi.Business.DTOs.Order.Payload;

namespace Cargodi.Business.Interfaces.Order;

public interface IOrderService
{
    Task<IEnumerable<GetOrderInfoDto>> GetAllAsync(CancellationToken cancellationToken);
	Task<GetOrderInfoDto> GetByIdAsync(long id, ClaimsPrincipal user, CancellationToken cancellationToken);
	Task<GetOrderDto> CreateAsync(long? customerId, ClaimsPrincipal user, UpdateOrderPayloadsDto orderDto, CancellationToken cancellationToken);
	Task<GetOrderDto> UpdateAsync(long id, ClaimsPrincipal user, UpdateOrderDto orderDto, CancellationToken cancellationToken);
	Task<GetOrderInfoDto> UpdatePayloadListAsync(long id, ClaimsPrincipal user, IEnumerable<UpdatePayloadDto> payloadDtos, CancellationToken cancellationToken);
	Task DeleteAsync(long id, ClaimsPrincipal user, CancellationToken cancellationToken);
	Task<GetOrderDto> SetStatusAsync(long id, string status, ClaimsPrincipal user, CancellationToken cancellationToken);
}