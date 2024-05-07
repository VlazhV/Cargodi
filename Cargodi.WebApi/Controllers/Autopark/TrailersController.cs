using Cargodi.Business.DTOs.Autopark.Trailer;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Autopark;

[Route("api/trailers")]
[ApiController]
[AuthorizeAdminManager]
public class TrailersController: ControllerBase
{
	private readonly ITrailerService _trailerService;
	
	public TrailersController(ITrailerService trailerService)
	{
		_trailerService = trailerService;
	}

		
	[HttpGet("{id}")]
	public async Task<ActionResult<GetTrailerAutoparkDto>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
	{
		return Ok(await _trailerService.GetByIdAsync(id, cancellationToken));
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetTrailerAutoparkDto>>> GetAllAsync(CancellationToken cancellationToken)
	{
		return Ok(await _trailerService.GetAllAsync(cancellationToken));
	}	
	
	[HttpPost]
	public async Task<ActionResult<GetTrailerDto>> CreateAsync([FromBody] UpdateTrailerDto trailerDto, CancellationToken cancellationToken)
	{
		return Ok(await _trailerService.CreateAsync(trailerDto, cancellationToken));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
	{
		await _trailerService.DeleteAsync(id, cancellationToken);
		
		return NoContent();
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<GetTrailerDto>> UpdateAsync([FromRoute] int id, UpdateTrailerDto trailerDto, CancellationToken cancellationToken)
	{
		return Ok(await _trailerService.UpdateAsync(id, trailerDto, cancellationToken));
	}		
}