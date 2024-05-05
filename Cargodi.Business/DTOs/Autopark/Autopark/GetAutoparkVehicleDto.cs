using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.DTOs.Autopark.Trailer;

namespace Cargodi.Business.DTOs.Autopark.Autopark;

public class GetAutoparkVehicleDto
{
	public int Id { get; set; }
	public string Address { get; set; } = null!;
	public int Capacity { get; set; }

	public List<GetCarDto> Cars { get; set; } = null!;
	public List<GetTrailerDto> Traliers { get; set; } = null!;
}