using System.Security.Claims;
using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.Business.Interfaces.Ship;
using Cargodi.DataAccess.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Ship;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class ShipsController: ControllerBase
{
    private readonly IShipService _shipService;
    
    public ShipsController(IShipService shipService)
    {
        _shipService = shipService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetShipDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager))
            return NotFound();

        return Ok(await _shipService.GetAllAsync(cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetShipDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager) && !User.IsInRole(Roles.Driver))
            return NotFound();

        return Ok(await _shipService.GetByIdAsync(id, cancellationToken));
    }
    
    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<GetShipDto>>> GetAllOfDriverAsync(CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Driver))
            return NotFound();
            
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var userId = long.Parse(userIdString);

        return Ok(await _shipService.GetAllOfDriverAsync(userId, cancellationToken));
    }
    
    [HttpPost]
    public async Task<ActionResult<IEnumerable<GetShipDto>>> CreateAsync([FromQuery] int driverCount, CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager))
            return NotFound();

        return Ok(await _shipService.GenerateAsync(User, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin))
            return NotFound();

        await _shipService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<GetShipDto>> UpdateShipAsync
        (int id, [FromBody] UpdateShipDto updateShipDto, CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager))
            return NotFound();

        return Ok(await _shipService.UpdateAsync(id, updateShipDto, cancellationToken));
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<GetShipDto>> MarkAsync(int id, CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager) && !User.IsInRole(Roles.Driver))
            return NotFound();

        return Ok(await _shipService.MarkAsync(id, User, cancellationToken));
    }
}
