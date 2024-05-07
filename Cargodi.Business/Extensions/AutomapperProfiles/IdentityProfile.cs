using AutoMapper;
using Cargodi.Business.DTOs.Identity;
using Cargodi.DataAccess.Entities;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class IdentityProfile: Profile
{
	public IdentityProfile()
	{
		CreateMap<User, UserDto>();
		CreateMap<UserDto, User>();
		CreateMap<SignupDto, User>();
		CreateMap<RegisterDto, User>();
		CreateMap<User, UserIdDto>();
		CreateMap<LoginDto, SignupDto>().ReverseMap();
	}
	
}