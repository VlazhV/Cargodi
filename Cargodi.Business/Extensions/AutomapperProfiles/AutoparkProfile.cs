using AutoMapper;
using Cargodi.Business.DTOs.Autopark.Autopark;
using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.DTOs.Autopark.Trailer;
using Cargodi.Business.DTOs.Common.AddressDtos;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Autopark;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class AutoparkProfile: Profile
{
	public AutoparkProfile()
	{
		CreateMap<Address, GetAddressDto>();
		CreateMap<UpdateAddressDto, Address>();
		
		CreateMap<Autopark, GetAutoparkDto>();
		CreateMap<Car, GetCarDto>();
		CreateMap<Trailer, GetTrailerDto>();
		
		CreateMap<Car, GetCarAutoparkDto>();
		CreateMap<Trailer, GetTrailerAutoparkDto>();
		CreateMap<Autopark, GetAutoparkVehicleDto>();
		
		CreateMap<UpdateCarDto, Car>();
		CreateMap<UpdateTrailerDto, Trailer>();
		CreateMap<UpdateAutoparkDto, Autopark>();
	}
}