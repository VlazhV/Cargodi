namespace Cargodi.Business.DTOs.Staff.Client;

public class GetClientDto
{
	public long Id { get; set; }

	public CredentialsDto Credentials { get; set; } = null!;
	public string Name { get; set; } = null!;
}