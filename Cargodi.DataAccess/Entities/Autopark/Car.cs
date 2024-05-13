namespace Cargodi.DataAccess.Entities.Autopark;

public class Car: ICarrier
{
	public int Id{ get; set; }
	public string LicenseNumber { get; set; } = null!;
	public string Mark { get; set; } = null!;	
	public int Range { get; set; } //km
	public int Carrying { get; set; } //g
	public int TankVolume { get; set; } //dm3	

	public int CapacityLength { get; set; } //mm	
	public int CapacityWidth { get; set; } //mm	
	public int CapacityHeight { get; set; } //mm

	public Autopark Autopark { get; set; } = null!;
	public int AutoparkId { get; set; }

	public Autopark ActualAutopark { get; set; } = null!;
	public int ActualAutoparkId { get; set; }

	public CarType CarType { get; set; } = null!;
	public int CarTypeId { get; set; }

	public ICollection<Ship.Ship>? Ships { get; set; }
	
	public bool CanInclude(int biggestLinearSize)
	{
		return CapacityHeight > biggestLinearSize || CapacityWidth > biggestLinearSize || CapacityLength > biggestLinearSize;
	}
	
	public int Capacity()
	{
		return CapacityHeight * CapacityLength * CapacityWidth;
	}
}
