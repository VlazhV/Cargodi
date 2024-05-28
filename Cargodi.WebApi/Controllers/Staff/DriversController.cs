using Cargodi.Business.DTOs.Staff;
using Cargodi.Business.DTOs.Staff.Driver;
using Cargodi.Business.Interfaces.Identity;
using Cargodi.DataAccess.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Staff;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DriversController: ControllerBase
{
    private readonly IUserService _userService;
    
    public DriversController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<GetDriverDto>> GetAllAsync([FromQuery] DriverFilter driverFilter, CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager))
            return NotFound();

        return Ok(await _userService.GetAllDriversAsync(driverFilter, cancellationToken));
    }
}
