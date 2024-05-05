namespace Cargodi.Business.DTOs.Order.Order;

public class UpdateOrderDto
{
    public string LoadAddress { get; set; } = null!;
	public string DeliverAddress { get; set; } = null!;
}