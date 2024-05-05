using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Interfaces.Order;
using Cargodi.DataAccess.Interfaces.Ship;
using Cargodi.DataAccess.Interfaces.Staff;
using Cargodi.DataAccess.Repositories.Autopark;
using Cargodi.DataAccess.Repositories.Order;
using Cargodi.DataAccess.Repositories.Ship;
using Cargodi.DataAccess.Repositories.Staff;

namespace Cargodi.WebApi;

public static class Startup
{
	public static void ConfigureRepositories(this IServiceCollection services)
	{
		//Autopark
		services.AddScoped<IAutoparkRepository, AutoparkRepository>();
		services.AddScoped<ICarRepository, CarRepository>();
		services.AddScoped<ITrailerRepository, TrailerRepository>();

		//Order
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IPayloadRepository, PayloadRepository>();

		//Ship
		services.AddScoped<IShipRepository, ShipRepository>();
		services.AddScoped<IStopRepository, StopRepository>();

		//Staff
		services.AddScoped<IDriverRepository, DriverRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
	}
}