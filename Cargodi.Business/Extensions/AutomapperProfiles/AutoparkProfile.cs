using AutoMapper;
using Cargodi.Business.DTOs.Autopark.Autopark;
using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.DTOs.Autopark.Trailer;
using Cargodi.DataAccess.Entities.Autopark;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class AutoparkProfile: Profile
{
	public AutoparkProfile()
	{
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