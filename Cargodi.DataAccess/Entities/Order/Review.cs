using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Entities.Order;

public class Review
{
    public int Id { get; set; }
    
    public long OrderId { get; set; }
    public Order Order { get; set; }
    
    public int Rating { get; set; }
    public string? Description { get; set; }
}
