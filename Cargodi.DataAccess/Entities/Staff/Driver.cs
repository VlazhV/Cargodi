namespace Cargodi.DataAccess.Entities.Staff;

public class Driver
{
	public int Id { get; set; }

	public Autopark.Autopark Autopark { get; set; } = null!;
	public int AutoparkId { get; set; }

	public Autopark.Autopark ActualAutopark { get; set; } = null!;
	public int AcutalAutoparkId { get; set; }

	public string License { get; set; } = null!;
	public string SecondName { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string? MiddleName { get; set; }

	public User User { get; set; } = null!;
	public long UserId { get; set; }

	public DriverStatus DriverStatus { get; set; } = null!;
	public int DriverStatusId { get; set; }
	
	public DateTime EmployDate { get; set; }
	public DateTime? FireDate { get; set; }
	
	public ICollection<Ship.Ship>? Ships { get; set; }
	public ICollection<Category> Categories { get; set; } = null!;
	
	
}