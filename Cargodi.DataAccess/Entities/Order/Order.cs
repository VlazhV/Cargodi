using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Entities.Order;

public class Order
{
	public long Id { get; set; }

	public DateTime Time { get; set; }
	public DateTime AcceptTime { get; set; }

	public Address LoadAddress { get; set; } = null!;
	public long LoadAddressId { get; set; }
	
	public Address DeliverAddress { get; set; } = null!;
	public long DeliverAddressId { get; set; }

	public List<Payload> Payloads { get; set; } = null!;

	public Client Client { get; set; } = null!;
	public long ClientId { get; set; } 

	public OrderStatus OrderStatus { get; set; } = null!;
	public ushort OrderStatusId { get; set; }
	
	public Operator? Operator { get; set; }
	public int OperatorId { get; set; }
}