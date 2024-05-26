using Cargodi.Business.DTOs.Common.AddressDtos;

namespace Cargodi.Business.DTOs.Order.Order;

public class GetOrderDto
{
    public long Id { get; set; }
	
	public DateTime Time { get; set; }
	public DateTime AcceptTime { get; set; }
	
	public GetAddressDto LoadAddress { get; set; } = null!;
	public GetAddressDto DeliverAddress { get; set; } = null!;

	public OrderStatusDto OrderStatus { get; set; } = null!;
}