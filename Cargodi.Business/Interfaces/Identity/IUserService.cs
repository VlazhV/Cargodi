using Cargodi.Business.DTOs.Identity;

namespace Cargodi.Business.Interfaces.Identity;

public interface IUserService
{
    Task<IEnumerable<UserIdDto>> GetAllAsync(string? userRole, CancellationToken cancellationToken);
	Task<UserDto> GetByIdAsync(string? id, string? userRole, CancellationToken cancellationToken);

	Task<UserDto> UpdateAsync(string? id, UserUpdateDto userUpdateDto, CancellationToken cancellationToken);
	Task UpdatePasswordAsync(string? id, PasswordDto passwordDto, CancellationToken cancellationToken);

	Task DeleteAsync(string? id, string? userRole, CancellationToken cancellationToken);
}