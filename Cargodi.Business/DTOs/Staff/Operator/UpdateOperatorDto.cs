namespace Cargodi.Business.DTOs.Staff.Operator;

public class UpdateOperatorDto
{
    public string SecondName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; } 
    public int AutoparkId { get; set; }
}