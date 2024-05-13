using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Entities.Ship;

public class Ship
{
	public int Id { get; set; }
	
	public int CarId { get; set; }
	public Car Car { get; set; } = null!;

	public int TrailerId { get; set; }
	public Trailer Trailer { get; set; } = null!;
	
	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }

	public Operator Operator { get; set; } = null!;
	public int OperatorId { get; set; }
	
	public int AutoparkStartId { get; set; }
	public Autopark.Autopark AutoparkStart { get; set; } = null!;
	
	public int AutoparkFinishId { get; set; }
	public Autopark.Autopark AutoparkFinish { get; set; } = null!;

	public ICollection<Driver> Drivers { get; set; } = null!;
	public ICollection<Stop> Stops { get; set; } = null!;
}