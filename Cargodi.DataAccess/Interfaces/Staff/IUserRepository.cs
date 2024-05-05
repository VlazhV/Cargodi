using Cargodi.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cargodi.DataAccess.Interfaces.Staff;

public interface IUserRepository: IRepository<User, long>
{
	Task<bool> DoesItExistAsync(User candidate, CancellationToken cancellationToken);

	Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken);
	
	Task<IList<string>> GetRolesAsync(User user);
	Task<User?> FindByNameAsync(string name);
	Task<bool> CheckPasswordAsync(User user, string password);
	Task<IdentityResult> CreateAsync(User user, string password);
	Task<IdentityResult> UpdateAsync(User user);
	Task<IdentityResult> AddToRoleAsync(User user, string role);
	Task<User?> FindByIdAsync(string id);
	Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
	Task<IdentityResult> DeleteAsync(User user);
}