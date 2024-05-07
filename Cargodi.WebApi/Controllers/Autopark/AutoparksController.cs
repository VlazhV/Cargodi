using Cargodi.Business.DTOs.Autopark.Autopark;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Autopark;

[Route("api/autoparks")]
[ApiController]
[AuthorizeAdminManager]
public class AutoparksController: ControllerBase
{
	private readonly IAutoparkService _autoparkService;
	
	public AutoparksController(IAutoparkService autoparkService)
	{
		_autoparkService = autoparkService;
	}
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetAutoparkVehicleDto>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
	{
		return Ok(await _autoparkService.GetByIdAsync(id, cancellationToken));
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetAutoparkVehicleDto>>> GetAllAsync(CancellationToken cancellationToken)
	{
		return Ok(await _autoparkService.GetAllAsync(cancellationToken));
	}	
	
	[HttpPost]
	public async Task<ActionResult<GetAutoparkVehicleDto>> CreateAsync([FromBody] UpdateAutoparkDto autoparkDto, CancellationToken cancellationToken)
	{
		return Ok(await _autoparkService.CreateAsync(autoparkDto, cancellationToken));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
	{
		await _autoparkService.DeleteAsync(id, cancellationToken);
		
		return NoContent();
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<GetAutoparkVehicleDto>> UpdateAsync([FromRoute] int id, UpdateAutoparkDto autoparkDto, CancellationToken cancellationToken)
	{
		return Ok(await _autoparkService.UpdateAsync(id, autoparkDto, cancellationToken));
	}
}