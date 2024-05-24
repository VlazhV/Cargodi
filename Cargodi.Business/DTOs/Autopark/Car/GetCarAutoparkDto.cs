using Cargodi.Business.DTOs.Autopark.Autopark;

namespace Cargodi.Business.DTOs.Autopark.Car;

public class GetCarAutoparkDto
{
    public int Id{ get; set; }
    public string LicenseNumber { get; set; } = null!;
    public string Mark { get; set; } = null!;	
    public int Range { get; set; } 
    public int Carrying { get; set; } 	
    public int TankVolume { get; set; } 
    public int CapacityLength { get; set; }
    public int CapacityWidth { get; set; }
    public int CapacityHeight { get; set; }

    public GetAutoparkDto Autopark { get; set; } = null!;
    public CarTypeDto CarType { get; set; } = null!;
}