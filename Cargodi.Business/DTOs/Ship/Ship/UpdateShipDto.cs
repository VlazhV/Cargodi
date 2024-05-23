using Cargodi.Business.DTOs.Ship.Stop;

namespace Cargodi.Business.DTOs.Ship.Ship;

public class UpdateShipDto
{
    public int Id { get; set; }
    public List<int> DriverIds { get; set; } = null!;
    public int CarId { get; set; }
    public int? TrailerId { get; set; }
    public int AutoparkStartId { get; set; }
    public int AutoparkFinishId { get; set; }
        
    public IEnumerable<UpdateStopDto> Stops { get; set; } = null!;
}