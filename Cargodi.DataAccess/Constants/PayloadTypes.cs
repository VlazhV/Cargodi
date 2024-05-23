using Cargodi.DataAccess.Entities.Order;

namespace Cargodi.DataAccess.Constants;

public static class PayloadTypes
{
	public static PayloadType Item { get => new PayloadType() { Id = 1, Name = "Item" }; }
	public static PayloadType Liquid { get => new PayloadType() { Id = 2, Name = "Liquid" }; }
	public static PayloadType Bulk { get => new PayloadType() { Id = 3, Name = "Bulk" }; }
}