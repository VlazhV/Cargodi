using Cargodi.Business.DTOs.Common.AddressDtos;

namespace Cargodi.Business.DTOs.Autopark.Autopark;

public class GetAutoparkDto
{
	public int Id { get; set; }
	public GetAddressDto Address { get; set; } = null!;
	public int Capacity { get; set; }
}