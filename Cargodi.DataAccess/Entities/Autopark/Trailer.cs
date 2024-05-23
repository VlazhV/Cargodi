namespace Cargodi.DataAccess.Entities.Autopark;

public class Trailer: ICarrier
{
	public int Id { get; set; }
	public string LicenseNumber { get; set; } = null!;

	public int CapacityLength { get; set; } //mm
	public int CapacityWidth { get; set; } //mm	
	public int CapacityHeight { get; set; } //mm	

	public int Carrying { get; set; }

	public Autopark ActualAutopark { get; set; } = null!;
	public int ActualAutoparkId { get; set; }

	public Autopark Autopark { get; set; } = null!;
	public int AutoparkId { get; set; }
	
	public required TrailerType TrailerType { get; set; }
	public int TrailerTypeId { get; set; }

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
