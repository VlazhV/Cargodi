using AutoMapper;
using Cargodi.Business.DTOs.Order;
using Cargodi.Business.DTOs.Order.Order;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.DataAccess.Entities.Order;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class OrderProfile: Profile
{
	public OrderProfile()
	{
        CreateMap<OrderStatus, OrderStatusDto>();
		CreateMap<Order, GetOrderDto>()						
		CreateMap<Payload, GetPayloadDto>();
		
		CreateMap<Order, GetOrderInfoDto>()
			.ForMember(dest => dest.OrderStatus,
				opt => opt.MapFrom(src => src.OrderStatus.Name
			));
			
		CreateMap<Payload, GetPayloadOrderDto>();	
		CreateMap<UpdateOrderPayloadsDto, Order>();
		CreateMap<UpdatePayloadDto, AddPayloadDto>();
		CreateMap<UpdatePayloadDto, Payload>();
		CreateMap<AddPayloadDto, Payload>();
			
		CreateMap<UpdateOrderDto, Order>();
	}   
	
}