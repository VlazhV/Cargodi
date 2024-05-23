using AutoMapper;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.DataAccess.Entities.Order;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class PayloadProfile : Profile
{
    public PayloadProfile()
    {
        CreateMap<PayloadTypeDto, PayloadType>();
    }
}