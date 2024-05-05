using Cargodi.Business.DTOs.Autopark.Autopark;

namespace Cargodi.Business.DTOs.Autopark.Trailer;

public class GetTrailerAutoparkDto
{
	public int Id { get; set; }
	public string LicenseNumber { get; set; } = null!;

	public int CapacityLength { get; set; } //mm
	public int CapacityWidth { get; set; } //mm	
	public int CapacityHeight { get; set; } //mm	

	public int Carrying { get; set; }

	public GetAutoparkDto Autopark { get; set; } = null!;
}