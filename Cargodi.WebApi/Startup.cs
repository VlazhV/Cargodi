using Cargodi.Business.Interfaces.Autopark;
using Cargodi.Business.Interfaces.Identity;
using Cargodi.Business.Interfaces.Order;
using Cargodi.Business.Services.Autopark;
using Cargodi.Business.Services.Identity;
using Cargodi.Business.Services.Order;
using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Interfaces.Autopark;
using Cargodi.DataAccess.Interfaces.Order;
using Cargodi.DataAccess.Interfaces.Ship;
using Cargodi.DataAccess.Interfaces.Staff;
using Cargodi.DataAccess.Repositories.Autopark;
using Cargodi.DataAccess.Repositories.Order;
using Cargodi.DataAccess.Repositories.Ship;
using Cargodi.DataAccess.Repositories.Staff;
using Microsoft.AspNetCore.Identity;

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
	
	public static void ConfigureServices(this IServiceCollection services)
	{
		//Autopark
		services.AddScoped<IAutoparkService, AutoparkService>();
		services.AddScoped<ICarService, CarService>();
		services.AddScoped<ITrailerService, TrailerService>();

		//Identity
		services.AddScoped<IIdentityService, IdentityService>();
		services.AddScoped<ITokenService, TokenService>();
		services.AddScoped<IUserService, UserService>();

		//Order
		services.AddScoped<IOrderService, OrderService>();
		services.AddScoped<IPayloadService, PayloadService>();
		
		services.AddIdentity<User, IdentityRole<long>>()
			.AddEntityFrameworkStores<DatabaseContext>()
			.AddUserManager<UserManager<User>>()
			.AddSignInManager<SignInManager<User>>();
		
	}
}