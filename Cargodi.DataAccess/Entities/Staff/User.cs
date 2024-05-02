using Cargodi.DataAccess.Entities.Staff;
using Microsoft.AspNetCore.Identity;

namespace Cargodi.DataAccess.Entities;

public class User: IdentityUser<long>
{
	public Client? Client { get; set; }
	public Driver? Driver { get; set; }
	public Operator? Operator { get; set; }
}