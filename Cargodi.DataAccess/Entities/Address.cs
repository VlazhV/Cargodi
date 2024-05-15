namespace Cargodi.DataAccess.Entities;

public class Address
{
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	
	public double Latitude { get; set; }
	public double Longitude { get; set; }
	
	public bool IsWest { get; set; }
	public bool IsNorth { get; set; }

	public ICollection<Autopark.Autopark>? Autoparks { get; set; }
	public ICollection<Order.Order>? LoadOrders { get; set; }
	public ICollection<Order.Order>? DeliveryOrders { get; set; }
	
	public override bool Equals(object? obj)
	{
		var e = 1 * 10e-6;
		var secondAddress = (Address)obj!;
		
		return secondAddress.Name.Equals(Name) &&
			Math.Abs(secondAddress.Latitude - Latitude) < e &&
			Math.Abs(secondAddress.Longitude - Longitude) < e &&
			secondAddress.IsWest == IsWest &&
			secondAddress.IsNorth == IsNorth; 
	}
}