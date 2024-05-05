using Cargodi.Business.DTOs.Identity;

namespace Cargodi.Business.Interfaces.Identity;

public interface IIdentityService
{
    Task<TokenDto> LoginAsync(LoginDto loginModel, CancellationToken cancellationToken);
	Task<TokenDto> SignUpAsync(SignupDto signupModel, CancellationToken cancellationToken);
	Task<UserDto> RegisterAsync(RegisterDto registerModel, string? registererRole, CancellationToken cancellationToken);
}