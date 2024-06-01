namespace Cargodi.DataAccess.Entities.Staff;

public class DriverCategory
{
    public int DriverId { get; set; }
    public string CategoryName { get; set; } = null!;
    
    public Driver Driver { get; set;}
    public Category Category { get; set; }
}
