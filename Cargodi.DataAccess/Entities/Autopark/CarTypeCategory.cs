using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Entities.Autopark;

public class CarTypeCategory
{
	public int CarTypeId { get; set; }
	public required string CategoryName { get; set; }

	public CarType CarType { get; set; } = null!;
	public Category Category { get; set; } = null!;
}