using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Entities.Autopark;

public class CarType
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	
	public List<Driver>? Drivers { get; set; }
	public List<Category> Categories { get; set; } = null!;
}
