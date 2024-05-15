using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Entities.Autopark;

public class Autopark
{
	public int Id { get; set; }
	public Address Address { get; set; } = null!;
	public long AddressId { get; set; }
	public int Capacity { get; set; }
	
	public List<Car>? Cars { get; set; }
	public List<Car>? ActualCars{ get; set; }
	
	public List<Trailer>? Trailers { get; set; }
	public List<Trailer>? ActualTrailers{ get; set; }
	
	public List<Operator>? Operators { get; set; }
	
	public List<Driver>? Drivers { get; set; }
	public List<Driver>? ActualDrivers { get; set; }

	public List<Ship.Ship> ShipStarts { get; set; } = null!;
	public List<Ship.Ship> ShipFinishes { get; set; } = null!;
}
