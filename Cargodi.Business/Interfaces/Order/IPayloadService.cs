using System.Security.Claims;
using Cargodi.Business.DTOs.Order.Payload;

namespace Cargodi.Business.Interfaces.Order;

public interface IPayloadService
{
    Task<GetPayloadOrderDto> GetByIdAsync(long id, ClaimsPrincipal user, CancellationToken cancellationToken);
	Task<IEnumerable<GetPayloadOrderDto>> GetAllAsync(CancellationToken cancellationToken);	
	Task<GetPayloadDto> UpdateAsync(long id, ClaimsPrincipal user, UpdatePayloadDto payloadDto, CancellationToken cancellationToken);
	Task DeleteAsync(long id, ClaimsPrincipal user, CancellationToken cancellationToken);
}