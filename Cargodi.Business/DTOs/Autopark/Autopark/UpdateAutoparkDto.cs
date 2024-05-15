using Cargodi.Business.DTOs.Common.AddressDtos;

namespace Cargodi.Business.DTOs.Autopark.Autopark;

public class UpdateAutoparkDto
{
	public UpdateAddressDto Address { get; set; } = null!;
	public int Capacity { get; set; }
}