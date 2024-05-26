using AutoMapper;
using Cargodi.Business.DTOs.Common.AddressDtos;
using Cargodi.Business.DTOs.Order;
using Cargodi.Business.DTOs.Order.Order;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.Business.DTOs.Staff.Operator;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class OrderProfile: Profile
{
    public OrderProfile()
    {
        CreateMap<PayloadType, PayloadTypeDto>().ReverseMap();
        CreateMap<UpdateOperatorDto, Operator>();
        CreateMap<Operator, GetOperatorDto>();
        CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
        CreateMap<Address, GetAddressDto>();
        CreateMap<UpdateAddressDto, Address>();
        
        CreateMap<Payload, GetPayloadDto>();
        CreateMap<Payload, GetPayloadOrderDto>();
        CreateMap<UpdatePayloadDto, AddPayloadDto>();
        CreateMap<UpdatePayloadDto, Payload>();
        CreateMap<AddPayloadDto, Payload>();
        
        CreateMap<UpdateOrderPayloadsDto, Order>();
        CreateMap<Order, GetOrderInfoDto>();
        CreateMap<UpdateOrderDto, Order>()
            .ForMember(dest => dest.Payloads, opt => opt.Ignore());
        CreateMap<Order, GetOrderDto>();  
    }   
    
}