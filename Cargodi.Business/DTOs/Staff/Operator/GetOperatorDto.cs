namespace Cargodi.Business.DTOs.Staff.Operator;

public class GetOperatorDto
{	
    public string SecondName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; } 
    
    public DateTime EmployDate { get; set; }
    public DateTime? FireDate { get; set; }
}