using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.DTOs.Autopark.Trailer;
using Cargodi.Business.DTOs.Common.AddressDtos;
using Cargodi.Business.DTOs.Staff.Driver;

namespace Cargodi.Business.DTOs.Autopark.Autopark;

public class GetAutoparkVehicleDto
{
	public int Id { get; set; }
	public GetAddressDto Address { get; set; } = null!;
	public int Capacity { get; set; }

	public List<GetCarDto> Cars { get; set; } = null!;
	public List<GetTrailerDto> Trailers { get; set; } = null!;
	public List<GetDriverDto> Drivers { get; set; } = null!;
	
	public List<GetCarDto> ActualCars { get; set; } = null!;
	public List<GetTrailerDto> ActualTrailers { get; set; } = null!;
	public List<GetDriverDto> ActualDrivers { get; set; } = null!;
}