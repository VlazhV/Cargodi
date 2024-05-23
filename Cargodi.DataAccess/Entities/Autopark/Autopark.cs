using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Entities.Autopark;

public class Autopark
{
	public int Id { get; set; }
	public Address Address { get; set; } = null!;
	public long AddressId { get; set; }
	public int Capacity { get; set; }

    public List<Car> Cars { get; set; } = null!;
	public List<Car> ActualCars{ get; set; } = null!;
	
	public List<Trailer> Trailers { get; set; } = null!;
	public List<Trailer> ActualTrailers{ get; set; } = null!;
	
	public List<Operator> Operators { get; set; } = null!;
	
	public List<Driver> Drivers { get; set; } = null!;
	public List<Driver> ActualDrivers { get; set; } = null!;

	public List<Ship.Ship> ShipStarts { get; set; } = null!;
	public List<Ship.Ship> ShipFinishes { get; set; } = null!;
}
