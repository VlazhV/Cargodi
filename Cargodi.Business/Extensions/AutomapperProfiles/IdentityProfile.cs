using AutoMapper;
using Cargodi.Business.DTOs.Identity;
using Cargodi.Business.DTOs.Staff.Client;
using Cargodi.Business.DTOs.Staff.Driver;
using Cargodi.Business.DTOs.Staff.Operator;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class IdentityProfile: Profile
{
    public IdentityProfile()
    {
        CreateMap<UpdateClientDto, Client>();
        CreateMap<UpdateDriverDto, Driver>();
        CreateMap<UpdateOperatorDto, Operator>();
        
        CreateMap<Client, GetClientDto>();
        CreateMap<Driver, GetDriverDto>();
        CreateMap<Operator, GetOperatorDto>();
        
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<SignupDto, User>();
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Driver, opt => opt.Ignore())
            .ForMember(dest => dest.Operator, opt => opt.Ignore());
        CreateMap<User, UserIdDto>();
        CreateMap<LoginDto, SignupDto>().ReverseMap();

        CreateMap<Client, GetClientUserDto>();
    }
    
}