using Cargodi.Business.DTOs.Staff.Client;
using Cargodi.Business.DTOs.Staff.Driver;
using Cargodi.Business.DTOs.Staff.Operator;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.Business.DTOs.Identity;

public class UserDto
{
    public long Id { get; set; }
    public string? UserName { get; set; }	
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public GetClientDto? Client { get; set; }
    public GetOperatorDto? Operator { get; set; }
    public GetDriverDto? Driver { get; set; }
}