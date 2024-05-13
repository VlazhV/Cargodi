using AutoMapper;
using Cargodi.Business.DTOs.Identity;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Identity;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.AspNetCore.Identity;

namespace Cargodi.Business.Services.Identity;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public UserService(
			IUserRepository userRepository,
			IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}
	
	public async Task DeleteAsync(string? id, string? userRole, CancellationToken cancellationToken)
	{
		if (userRole != Roles.Admin)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		if (id is null)
			throw new ApiException("BadRequest", ApiException.BadRequest);

		var user = await _userRepository.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		await HandleIdentityResultAsync(_userRepository.DeleteAsync(user));
	}

	public async Task<IEnumerable<UserIdDto>> GetAllAsync(string? userRole, CancellationToken cancellationToken)
	{
		if (userRole != Roles.Admin && userRole != Roles.Manager)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);

		var users = await _userRepository.GetAllAsync(cancellationToken);

		return _mapper.Map<IEnumerable<UserIdDto>>(users);
	}

	public async Task<UserDto> GetByIdAsync(string? id, string? userRole, CancellationToken cancellationToken)
	{
		if (userRole is not null)
			if (userRole != Roles.Admin && userRole != Roles.Manager)
				throw new ApiException("Not Found", ApiException.NotFound);
			
		var user = await _userRepository.FindByIdAsync(id!) 
			?? throw new ApiException("User is not found", ApiException.NotFound);
			
		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userRepository.GetRolesAsync(user)).First();
		
		return userDto;
	}

	public async Task<UserDto> UpdateAsync(string? id, UserUpdateDto userUpdateDto, CancellationToken cancellationToken)
	{
		if (id is null)
		 	throw new ApiException("Unauthorized", ApiException.Unauthorized);
		
		var user = await _userRepository.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		user.Email = userUpdateDto.Email;
		user.PhoneNumber = userUpdateDto.PhoneNumber;
		user.UserName = userUpdateDto.UserName;
		
		await HandleIdentityResultAsync(_userRepository.UpdateAsync(user));

		var userDto = _mapper.Map<UserDto>(user);
		userDto.Role = (await _userRepository.GetRolesAsync(user)).First();
		
		return userDto;
	}

	public async Task UpdatePasswordAsync(string? id, PasswordDto passwordDto, CancellationToken cancellationToken)
	{
		if (id is null)
			throw new ApiException("Unauthorized", ApiException.Unauthorized);

		var user = await _userRepository.FindByIdAsync(id)
			?? throw new ApiException("User is not found", ApiException.NotFound);

		await HandleIdentityResultAsync(_userRepository.ChangePasswordAsync(user, passwordDto.OldPassword!, passwordDto.NewPassword!));
	}

	private async Task HandleIdentityResultAsync(Task<IdentityResult> taskResult)
	{
		var result = await taskResult;
		if (!result.Succeeded)
		{			
			var message = result.Errors.Select(e => e.Description)
				.Aggregate((a, c) => $"{a}{c}");

			throw new ApiException(message, ApiException.BadRequest);
		}
	}
}