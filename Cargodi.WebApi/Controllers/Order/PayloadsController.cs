using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.Business.Interfaces.Order;
using Cargodi.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Order;


[Route("api/payloads")]
[ApiController]
[Authorize]
public class PayloadsController: ControllerBase
{
	private readonly IPayloadService _payloadService;
	
	public PayloadsController(IPayloadService payloadService)
	{
		_payloadService = payloadService;
	}
	
	[HttpGet]
	[AuthorizeAdminManager]
	public async Task<ActionResult<IEnumerable<GetPayloadOrderDto>>> GetAllAsync(CancellationToken cancellationToken)
	{
		return Ok(await _payloadService.GetAllAsync(cancellationToken));
	}
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetPayloadOrderDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
	{
		return Ok(await _payloadService.GetByIdAsync(id, User, cancellationToken));		
	}
	
	[HttpPut("{id}")]
	public async Task<ActionResult<GetPayloadDto>> UpdateAsync(long id, [FromBody] UpdatePayloadDto payloadDto, CancellationToken cancellationToken)
	{
		return Ok(await _payloadService.UpdateAsync(id, User, payloadDto, cancellationToken));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
	{
		await _payloadService.DeleteAsync(id, User, cancellationToken);

		return NoContent();
	}
}