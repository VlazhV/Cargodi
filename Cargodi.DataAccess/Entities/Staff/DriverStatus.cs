namespace Cargodi.DataAccess.Entities.Staff;

public class DriverStatus
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;

	public ICollection<Driver>? Drivers { get; set; }
}