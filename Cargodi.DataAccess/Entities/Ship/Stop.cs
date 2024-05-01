using Cargodi.DataAccess.Entities.Order;

namespace Cargodi.DataAccess.Entities.Ship;

public class Stop
{
	public long Id { get; set; }
	public short Number { get; set; }
	
	public int ShipId { get; set; }
	public Ship Ship { get; set; } = null!;
	
	public long OrderId { get; set; }
	public Order.Order Order { get; set; } = null!;
	
	public DateTime? Time { get; set; }
}