namespace Cargodi.DataAccess.Entities.Order;

public class OrderStatus
{
	public ushort Id { get; set; }
	public string Name { get; set; } = null!;   
	 
	public ICollection<Order>? Orders { get; set; }
}