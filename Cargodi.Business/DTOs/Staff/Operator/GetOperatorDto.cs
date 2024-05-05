namespace Cargodi.Business.DTOs.Staff.Operator;

public class GetOperatorDto
{
	public int Id { get; set; }
	
	public string SecondName { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string? MiddleName { get; set; } 
	
	public CredentialsDto Credentials { get; set; } = null!;
}