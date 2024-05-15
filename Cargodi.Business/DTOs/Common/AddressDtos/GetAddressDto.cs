namespace Cargodi.Business.DTOs.Common.AddressDtos;

public class GetAddressDto
{
    public long Id { get; set; }
	public string Name { get; set; } = null!;
	
	public float Latitude { get; set; }
	public float Longitude { get; set; }
	
	public bool IsWest { get; set; }
	public bool IsNorth { get; set; }
}