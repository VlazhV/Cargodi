namespace Cargodi.DataAccess.Entities.Order;

public class Payload
{
	public long Id { get; set; }

	public int Length { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }
	public int Weight { get; set; }

	public string? Description { get; set; }

	public Order Order { get; set; } = null!;
	public long OrderId { get; set; }

    public PayloadType PayloadType { get; set; } = null!;
	public int PayloadTypeId { get; set; }
}