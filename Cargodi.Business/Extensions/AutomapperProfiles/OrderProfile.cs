using AutoMapper;
using Cargodi.Business.DTOs.Order;
using Cargodi.Business.DTOs.Order.Order;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.Business.DTOs.Staff.Operator;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class OrderProfile: Profile
{
    public OrderProfile()
    {
        CreateMap<PayloadType, PayloadTypeDto>();
        CreateMap<UpdateOperatorDto, Operator>();
        CreateMap<Operator, GetOperatorDto>();
        
        CreateMap<Payload, GetPayloadDto>();
        CreateMap<Payload, GetPayloadOrderDto>();	
        CreateMap<UpdateOrderPayloadsDto, Order>();
        CreateMap<UpdatePayloadDto, AddPayloadDto>();
        CreateMap<UpdatePayloadDto, Payload>();
        CreateMap<AddPayloadDto, Payload>();

        CreateMap<Order, GetOrderInfoDto>();
        CreateMap<UpdateOrderDto, Order>();
        CreateMap<OrderStatus, OrderStatusDto>();
        CreateMap<Order, GetOrderDto>();  
    }   
    
}