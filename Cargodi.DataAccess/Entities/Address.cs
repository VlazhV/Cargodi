namespace Cargodi.DataAccess.Entities;

public class Address
{
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	
	public float Latitude { get; set; }
	public float Longitude { get; set; }
	
	public bool IsWest { get; set; }
	public bool IsNorth { get; set; }

	public ICollection<Autopark.Autopark>? Autoparks { get; set; }
	public ICollection<Order.Order>? LoadOrders { get; set; }
	public ICollection<Order.Order>? DeliveryOrders { get; set; }
}