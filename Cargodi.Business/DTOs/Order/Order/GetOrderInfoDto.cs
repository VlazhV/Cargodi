using Cargodi.Business.DTOs.Common.AddressDtos;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.Business.DTOs.Staff.Client;
using Cargodi.Business.DTOs.Staff.Operator;

namespace Cargodi.Business.DTOs.Order.Order;

public class GetOrderInfoDto
{
    public long Id { get; set; }

	public DateTime Time { get; set; }
	public DateTime? AcceptTime { get; set; }

    public GetAddressDto LoadAddress { get; set; } = null!;	
	public GetAddressDto DeliverAddress { get; set; } = null!;

	public List<GetPayloadDto> Payloads { get; set; } = null!;

	public GetClientDto Client { get; set; } = null!;
	public long ClientId { get; set; } 

	public OrderStatusDto OrderStatus { get; set; } = null!;
	
	public GetOperatorDto? Operator { get; set; } 
}