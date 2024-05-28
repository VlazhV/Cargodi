using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.Business.DTOs.Staff;

public class DriverFilter
{
    public bool? IsFree { get; set; }
    public int? ActualAutoparkId { get; set; }
    public bool? IsWork { get; set; }
}
