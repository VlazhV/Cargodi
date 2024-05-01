namespace Cargodi.DataAccess.Entities.Staff;

public class Client
{
	public long Id { get; set; }

	public User User { get; set; } = null!;
	public long UserId { get; set; }
}