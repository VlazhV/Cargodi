using Cargodi.Business.DTOs.Common.AddressDtos;

namespace Cargodi.Business.DTOs.Order.Order;

public class UpdateOrderDto
{
    public UpdateAddressDto LoadAddress { get; set; } = null!;
	public UpdateAddressDto DeliverAddress { get; set; } = null!;
}