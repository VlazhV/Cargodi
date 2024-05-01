using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Cargodi.DataAccess.Entities.Staff;

public class Category
{
	[Key]
	public string Name { get; set; } = null!;
	public ICollection<Driver>? Drivers { get; set; }
}