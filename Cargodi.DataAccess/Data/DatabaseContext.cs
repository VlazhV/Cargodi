using System.Reflection;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Entities.Ship;
using Cargodi.DataAccess.Entities.Staff;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Data;

public class DatabaseContext: IdentityDbContext<User, IdentityRole<long>, long>
{
	public DbSet<Autopark> Autoparks { get; set; }
	public DbSet<Car> Cars{ get; set; }
	public DbSet<Trailer> Trailers{ get; set; }
	
	public DbSet<Order> Orders { get; set; }
	public override DbSet<User> Users{ get; set; }
	public DbSet<Payload> Payloads { get; set; }
	public DbSet<OrderStatus> OrderStatuses { get; set; }
	
	public DbSet<Ship> Ships { get; set; }
	public DbSet<Stop> Stops { get; set; }
	
	public DbSet<Category> Categories { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<Driver> Drivers { get; set; }
	public DbSet<DriverStatus> DriverStatuses { get; set; }
	public DbSet<Operator> Operators { get; set; }
	
	public DbSet<Address> Addresses { get; set; }
	public DbSet<CarTypeCategory> CarTypesCategories { get; set; }
	
	
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}