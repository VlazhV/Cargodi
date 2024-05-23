using Cargodi.Business.DTOs.Common.AddressDtos;
using Cargodi.Business.DTOs.Order.Order;

namespace Cargodi.Business.DTOs.Ship.Stop;

public class GetStopDto
{
    public long Id { get; set; }
    public int Number { get; set; }
    public GetOrderDto Order { get; set; } = null!;
    public GetAddressDto Address { get; set; } = null!;
}
