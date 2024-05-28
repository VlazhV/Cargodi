using Cargodi.Business.DTOs.Staff.Client;
using Cargodi.Business.Interfaces.Identity;
using Cargodi.DataAccess.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Staff;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientsController: ControllerBase
{
    private readonly IUserService _userService;
    
    public ClientsController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetClientUserDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager))
            return NotFound();

        return Ok(await _userService.GetAllClientsAsync(cancellationToken));
    }
}
