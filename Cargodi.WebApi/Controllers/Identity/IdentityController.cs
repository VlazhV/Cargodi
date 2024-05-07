using System.Security.Claims;
using Cargodi.Business.DTOs.Identity;
using Cargodi.Business.Interfaces.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Identity;

[ApiController]
[Route("api")]
public class IdentityController: ControllerBase
{
	private IIdentityService _identityService;
	public IdentityController(IIdentityService userService)
	{
		_identityService = userService;
	}


	[HttpPost("login")]
	public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginModel, CancellationToken cancellationToken)
	{
		return Ok(await _identityService.LoginAsync(loginModel, cancellationToken));
	}

	[HttpPost("sign-up")]
	public async Task<ActionResult<TokenDto>> SignUp([FromBody] SignupDto signupModel, CancellationToken cancellationToken)
	{
		return Ok(await _identityService.SignUpAsync(signupModel, cancellationToken));
	}

	[HttpPost("register")]
	[Authorize]
	public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
	{				
		return Ok(await _identityService.RegisterAsync(registerDto, User.FindFirst(ClaimTypes.Role)?.Value, cancellationToken));		
	}
}