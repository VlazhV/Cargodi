using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Cargodi.DataAccess.Entities.Autopark;

namespace Cargodi.DataAccess.Entities.Staff;

public class Category
{
	[Key]
	public string Name { get; set; } = null!;
	public ICollection<Driver>? Drivers { get; set; }
	public List<CarTypeCategory> CarTypeCategories { get; set; } = null!;
}