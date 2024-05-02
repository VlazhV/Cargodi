using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
	public void Configure(EntityTypeBuilder<Car> builder)
	{
		builder.HasIndex(car => car.LicenseNumber).IsUnique();	
		
		builder.HasMany(e => e.Ships)
			.WithOne(e => e.Car)
			.OnDelete(DeleteBehavior.NoAction);
	}
}