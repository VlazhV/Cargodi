using AutoMapper;
using Cargodi.Business.DTOs.Ship.Ship;
using Cargodi.Business.DTOs.Ship.Stop;
using Cargodi.DataAccess.Entities.Ship;

namespace Cargodi.Business.Extensions.AutomapperProfiles;

public class ShipProfile: Profile
{
    public ShipProfile()
    {
        CreateMap<Stop, GetStopDto>();
        CreateMap<Ship, GetShipDto>();

        CreateMap<UpdateStopDto, Stop>();
        CreateMap<UpdateShipDto, Ship>();
        
    }
}