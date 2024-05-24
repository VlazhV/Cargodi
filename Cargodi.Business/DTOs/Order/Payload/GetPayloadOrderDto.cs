using Cargodi.Business.DTOs.Order.Order;

namespace Cargodi.Business.DTOs.Order.Payload;

public class GetPayloadOrderDto
{
	public long Id { get; set; }
	
	public int Length { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }
	public int Weight { get; set; }
	
	public string? Description { get; set; }
	
	public GetOrderDto Order { get; set; } = null!;
	
	public PayloadTypeDto PayloadType { get; set; } = null!;
}