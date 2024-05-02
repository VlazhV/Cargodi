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
	
	public DateTime EmployDate { get; set; }
	public DateTime? FireDate { get; set; }
	
	public ICollection<Order.Order>? Orders { get; set; }
}