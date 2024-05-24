using Cargodi.Business.DTOs.Staff.Client;
using Cargodi.Business.DTOs.Staff.Driver;
using Cargodi.Business.DTOs.Staff.Operator;

namespace Cargodi.Business.DTOs.Identity;

public class RegisterDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    
    public UpdateClientDto? Client { get; set; }
    public UpdateDriverDto? Driver { get; set; }
    public UpdateOperatorDto? Operator { get; set; }
}