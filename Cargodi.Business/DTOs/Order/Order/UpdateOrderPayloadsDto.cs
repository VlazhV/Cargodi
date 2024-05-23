using Cargodi.Business.DTOs.Common.AddressDtos;
using Cargodi.Business.DTOs.Order.Payload;

namespace Cargodi.Business.DTOs.Order.Order;

public class UpdateOrderPayloadsDto
{
	public UpdateAddressDto LoadAddress { get; set; } = null!;
	public UpdateAddressDto DeliverAddress { get; set; } = null!;
	
	public List<UpdatePayloadDto> Payloads { get; set; } = null!;
}