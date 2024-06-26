namespace Cargodi.Business.DTOs.Autopark.Trailer;

public class GetTrailerDto
{
    public int Id { get; set; }
    public string LicenseNumber { get; set; } = null!;

    public int CapacityLength { get; set; } //mm
    public int CapacityWidth { get; set; } //mm	
    public int CapacityHeight { get; set; } //mm	

    public int Carrying { get; set; }
    
    public int AutoparkId { get; set; }
    public int ActualAutoparkId { get; set; }

    public TrailerTypeDto TrailerType { get; set; } = null!;
}