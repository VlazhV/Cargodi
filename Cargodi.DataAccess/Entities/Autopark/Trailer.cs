namespace Cargodi.DataAccess.Entities.Autopark;

public class Trailer
{
	public int Id { get; set; }
	public string LicenseNumber { get; set; } = null!;

	public int CapacityLength { get; set; } //mm
	public int CapacityWidth { get; set; } //mm	
	public int CapacityHeight { get; set; } //mm	

	public int Carrying { get; set; }

	public Autopark Autopark { get; set; } = null!;
	public int AutoparkId { get; set; }

	public ICollection<Ship.Ship>? Ships { get; set; }
}
