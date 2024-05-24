using Cargodi.Business.DTOs.Autopark.Autopark;

namespace Cargodi.Business.DTOs.Staff.Driver;

public class GetDriverDto 
{
    public int Id { get; set; }

    public string License { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    
    public DateTime EmployDate { get; set; }
    public DateTime? FireDate { get; set; }
}
