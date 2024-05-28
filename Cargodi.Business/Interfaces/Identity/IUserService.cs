using Cargodi.Business.DTOs.Identity;
using Cargodi.Business.DTOs.Staff;
using Cargodi.Business.DTOs.Staff.Client;
using Cargodi.Business.DTOs.Staff.Driver;

namespace Cargodi.Business.Interfaces.Identity;

public interface IUserService
{
    Task<IEnumerable<UserIdDto>> GetAllAsync(string? userRole, CancellationToken cancellationToken);
    Task<UserDto> GetByIdAsync(string? id, string? userRole, CancellationToken cancellationToken);

    Task<UserDto> UpdateAsync(string? id, UserUpdateDto userUpdateDto, CancellationToken cancellationToken);
    Task UpdatePasswordAsync(string? id, PasswordDto passwordDto, CancellationToken cancellationToken);

    Task DeleteAsync(string? id, string? userRole, CancellationToken cancellationToken);

    Task<IEnumerable<GetDriverDto>> GetAllDriversAsync(DriverFilter driver, CancellationToken cancellationToken);
    Task<IEnumerable<GetClientUserDto>> GetAllClientsAsync(CancellationToken cancellationToken);
}