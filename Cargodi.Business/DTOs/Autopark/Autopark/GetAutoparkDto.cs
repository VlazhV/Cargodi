namespace Cargodi.Business.DTOs.Autopark.Autopark;

public class GetAutoparkDto
{
	public int Id { get; set; }
	public string Address { get; set; } = null!;
	public int Capacity { get; set; }
}