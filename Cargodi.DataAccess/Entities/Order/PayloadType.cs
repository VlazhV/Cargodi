using Cargodi.DataAccess.Entities.Autopark;

namespace Cargodi.DataAccess.Entities.Order;

public class PayloadType
{
	public int Id { get; set; }
	public required string Name { get; set; }
	
	public List<Payload>? Payloads { get; set; }
	public List<CarType>? CarTypes { get; set; }
}