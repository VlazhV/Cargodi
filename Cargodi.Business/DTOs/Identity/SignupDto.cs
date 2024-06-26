using Cargodi.Business.DTOs.Staff.Client;
using Cargodi.Business.DTOs.Staff.Driver;
using Cargodi.Business.DTOs.Staff.Operator;

namespace Cargodi.Business.DTOs.Identity;

public class SignupDto
{
    public string? UserName { get; set; } 
    public string? Password{ get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    
    public UpdateClientDto? Client { get; set; }
}