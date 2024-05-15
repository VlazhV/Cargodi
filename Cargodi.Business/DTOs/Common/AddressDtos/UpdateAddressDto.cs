namespace Cargodi.Business.DTOs.Common.AddressDtos;

public class UpdateAddressDto
{
	public string Name { get; set; } = null!;
	
	public float Latitude { get; set; }
	public float Longitude { get; set; }
	
	public bool IsWest { get; set; }
	public bool IsNorth { get; set; }
}