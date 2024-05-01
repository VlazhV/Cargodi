namespace Cargodi.DataAccess.Entities.Staff;

public class Operator
{
	public int Id { get; set; }
	
	public int AutoparkId { get; set; }
	public Autopark.Autopark Autopark { get; set; } = null!;

	public User User { get; set; } = null!;
	public long UserId { get; set; }
	
	public string SecondName { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string? MiddleName { get; set; } 
}