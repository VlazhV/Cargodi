using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.Interfaces.Autopark;
using Cargodi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Autopark;

[Route("api/cars")]
[ApiController]
[AuthorizeAdminManager]
public class CarsController: ControllerBase
{
	private readonly ICarService _carService;
	
	public CarsController(ICarService carService)
	{
		_carService = carService;
	}
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetCarAutoparkDto>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
	{
		return Ok(await _carService.GetByIdAsync(id, cancellationToken));
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetCarAutoparkDto>>> GetAllAsync(CancellationToken cancellationToken)
	{
		return Ok(await _carService.GetAllAsync(cancellationToken));
	}
	
	[HttpPost]
	public async Task<ActionResult<GetCarDto>> CreateAsync([FromBody] UpdateCarDto carDto, CancellationToken cancellationToken)
	{
		return Ok(await _carService.CreateAsync(carDto, cancellationToken));
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
	{
		await _carService.DeleteAsync(id, cancellationToken);
		
		return NoContent();
	}
	
	[HttpPut("{id}")]
	public async Task<ActionResult<GetCarDto>> UpdateAsync([FromRoute] int id, UpdateCarDto carDto, CancellationToken cancellationToken)
	{
		return Ok(await _carService.UpdateAsync(id, carDto, cancellationToken));	
	}
}