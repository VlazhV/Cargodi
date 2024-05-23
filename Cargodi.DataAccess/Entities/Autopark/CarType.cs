using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Entities.Autopark;

public class CarType
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	
	public List<Driver>? Cars { get; set; }
	public List<CarTypeCategory> CarTypeCategories { get; set; } = null!;
}
