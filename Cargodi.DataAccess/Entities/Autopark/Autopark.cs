namespace Cargodi.DataAccess.Entities.Autopark;

public class Autopark
{
	public int Id { get; set; }
	public Address Address { get; set; } = null!;
	public long AddressId { get; set; }
	
	public List<Car>? Cars { get; set; }
	public List<Trailer>? Trailers { get; set; }
}
