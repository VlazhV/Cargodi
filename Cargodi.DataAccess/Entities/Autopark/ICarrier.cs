namespace Cargodi.DataAccess.Entities.Autopark;

public interface ICarrier
{
	int CapacityLength { get; set; } //mm	
	int CapacityWidth { get; set; } //mm	
	int CapacityHeight { get; set; } //mm
	
	int Carrying { get; set; } //g

	int Capacity();
	bool CanInclude(int biggestLinearSize);
}