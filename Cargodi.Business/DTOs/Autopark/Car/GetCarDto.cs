namespace Cargodi.Business.DTOs.Autopark.Car;

public class GetCarDto
{
	public int Id { get; set; }
	public string LicenseNumber { get; set; } = null!;
	public string Mark { get; set; } = null!;
	public int Range { get; set; } //km
	public int Carrying { get; set; } //g
	public int TankVolume { get; set; } //dm3	

	public int CapacityLength { get; set; } //mm	
	public int CapacityWidth { get; set; } //mm	
	public int CapacityHeight { get; set; } //mm

	public string CarType { get; set; } = null!;
}