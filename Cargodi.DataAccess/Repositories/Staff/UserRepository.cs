using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.AspNetCore.Identity;

namespace Cargodi.DataAccess.Repositories.Staff;

public class UserRepository : RepositoryBase<User, long>, IUserRepository
{
	private readonly UserManager<User> _userManager;
	public UserRepository(DatabaseContext db, UserManager<User> userManager) : base(db)
	{
		_userManager = userManager;
	}


	public async Task<IdentityResult> AddToRoleAsync(User user, string role)
	{
		return await _userManager.AddToRoleAsync(user, role);
	}

	public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
	{
		return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
	}

	public async Task<bool> CheckPasswordAsync(User user, string password)
	{
		return await _userManager.CheckPasswordAsync(user, password);;
	}

	public async Task<IdentityResult> CreateAsync(User user, string password)
	{
		return await _userManager.CreateAsync(user, password);
	}

	public async Task<IdentityResult> DeleteAsync(User user)
	{
		return await _userManager.DeleteAsync(user);
	}

	public bool DoesItExist(User candidate)
	{
		return _db.Users.Any(u => u.Email!.Equals(candidate.Email)
					|| u.UserName!.Equals(candidate.UserName)
					|| u.PhoneNumber!.Equals(candidate.PhoneNumber));
	}

	public async Task<User?> FindByIdAsync(string id)
	{
		return await _userManager.FindByIdAsync(id);
	}

	public async Task<User?> FindByNameAsync(string name)
	{
		return await _userManager.FindByNameAsync(name);
	}

	public async Task<IList<string>> GetRolesAsync(User user)
	{
		return await _userManager.GetRolesAsync(user);
	}

	public async Task<IdentityResult> UpdateAsync(User user)
	{
		return await _userManager.UpdateAsync(user);
	}

}