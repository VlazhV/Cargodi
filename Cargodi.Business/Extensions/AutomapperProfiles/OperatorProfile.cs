using AutoMapper;
using Cargodi.Business.DTOs.Staff.Operator;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class OperatorProfile: Profile
{
	public OperatorProfile()
	{
		CreateMap<UpdateOperatorDto, Operator>();
		CreateMap<Operator, GetOperatorDto>()
			.ForMember(dist => dist.Credentials, opt => opt.MapFrom(src => src.User));
	}
}