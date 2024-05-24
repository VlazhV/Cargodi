using Cargodi.DataAccess.Entities.Order;

namespace Cargodi.DataAccess.Entities.Autopark;

public class TrailerType
{
	public int Id { get; set; }
	public required string Name { get; set; }
	
	public List<Trailer>? Trailers { get; set; }
}