namespace Cargodi.Business.DTOs.Order.Order;

public class GetOrderDto
{
    public long Id { get; set; }
	
	public DateTime Time { get; set; }
	public DateTime AcceptTime { get; set; }
	
	public string LoadAddress { get; set; } = null!;
	public string DeliverAddress { get; set; } = null!;

	public OrderStatusDto OrderStatus { get; set; } = null!;
}