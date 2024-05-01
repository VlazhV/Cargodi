using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Data;

public class DatabaseContext: IdentityDbContext<User, IdentityRole<long>, long>
{
	public DbSet<Autopark> Autoparks { get; set; }
	public DbSet<Car> Cars{ get; set; }
	public DbSet<Trailer> Trailers{ get; set; }
	public DbSet<CarInShipSchedule> CarSchedule { get; set; }
	public DbSet<TrailerInShipSchedule> TrailerSchedule { get; set; }
	
	public DbSet<Order> Orders { get; set; }
	public override DbSet<User> Users{ get; set; }
	public DbSet<Payload> Payloads { get; set; }
	public DbSet<OrderStatus> OrderStatuses { get; set; }
	
	
	
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
}