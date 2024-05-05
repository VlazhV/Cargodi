using Cargodi.Business.DTOs.Order.Payload;

namespace Cargodi.Business.DTOs.Order.Order;

public class UpdateOrderPayloadDto
{
	public string LoadAddress { get; set; } = null!;
	public string DeliverAddress { get; set; } = null!;
	
	public List<UpdatePayloadDto> Payloads { get; set; } = null!;
}