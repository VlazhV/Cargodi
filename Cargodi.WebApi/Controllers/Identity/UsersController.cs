using System.Security.Claims;
using Cargodi.Business.DTOs.Identity;
using Cargodi.Business.Interfaces.Identity;
using Cargodi.DataAccess.Constants;
using Cargodi.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Identity;

[ApiController]
[Route("api")]
public class UsersController: ControllerBase
{
	private readonly IUserService _userSerivce;
	public UsersController
		(IUserService userService)
	{
		_userSerivce = userService;
	}
	
	[HttpGet("users")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<UserIdDto>>> GetAllAsync(CancellationToken cancellationToken)
	{
		if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager))
			return NotFound();
		
		return Ok(await _userSerivce.GetAllAsync(User.FindFirst(ClaimTypes.Role)?.Value, cancellationToken));
	}
	
	[HttpGet("users/{id}")]
	[Authorize]
	public async Task<ActionResult<UserDto>> GetByIdAsync(string id, CancellationToken cancellationToken) 
	{	
		if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager))
			return NotFound();
			
		return Ok(await _userSerivce.GetByIdAsync(id, User.FindFirst(ClaimTypes.Role)?.Value, cancellationToken));
	}
	
	[HttpGet("profile")]
	[Authorize]
	public async Task<ActionResult<UserDto>> GetProfileAsync(CancellationToken cancellationToken)
	{
		return Ok(await _userSerivce.GetByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null, cancellationToken));
	}
	
	[HttpPut("profile")]
	[Authorize]
	public async Task<ActionResult<UserDto>> UpdateProfileAsync([FromBody] UserUpdateDto userDto, CancellationToken cancellationToken)	
	{
		return Ok(await _userSerivce.UpdateAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, userDto, cancellationToken));
	}
	
	[HttpPut("profile/password")]
	[Authorize]
	public async Task<ActionResult> UpdatePasswordAsync([FromBody] PasswordDto passwordDto, CancellationToken cancellationToken)
	{
		await _userSerivce.UpdatePasswordAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, passwordDto, cancellationToken);
		return Ok();
	}
	
	[HttpDelete("users/{id}")]
	[Authorize]
	public async Task<ActionResult> DeleteUserAsync ([FromRoute] string? id, CancellationToken cancellationToken)
	{
		await _userSerivce.DeleteAsync(id, User.FindFirst(ClaimTypes.Role)?.Value, cancellationToken);
		return Ok();
	}
}