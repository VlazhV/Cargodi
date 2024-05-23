using Cargodi.Business.DTOs.Autopark.Autopark;
using Cargodi.Business.DTOs.Autopark.Car;
using Cargodi.Business.DTOs.Autopark.Trailer;
using Cargodi.Business.DTOs.Ship.Stop;
using Cargodi.Business.DTOs.Staff.Driver;
using Cargodi.Business.DTOs.Staff.Operator;

namespace Cargodi.Business.DTOs.Ship.Ship;

public class GetShipDto
{
    public int Id { get; set; }
    public List<GetDriverDto> Drivers { get; set; } = null!;
    public GetCarDto Car { get; set; } = null!;
    public GetTrailerDto? Trailer { get; set; }
    public GetAutoparkDto AutoparkStart { get; set; } = null!;
    public GetAutoparkDto AutoparkFinish { get; set; } = null!;
    public GetOperatorDto Operator { get; set; } = null!;
    public DateTime? Start { get; set; }
    public DateTime? Finish { get; set; }
    public IEnumerable<GetStopDto> Stops { get; set; } = null!;
}