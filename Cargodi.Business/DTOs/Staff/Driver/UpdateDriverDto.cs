namespace Cargodi.Business.DTOs.Staff.Driver;

public class UpdateDriverDto
{
    public string License { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public int AutoparkId { get; set; }
    public int ActualAutoparkId { get; set; }

}
