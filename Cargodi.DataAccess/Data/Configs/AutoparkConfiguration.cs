using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class AutoparkConfiguration : IEntityTypeConfiguration<Autopark>
{
	public void Configure(EntityTypeBuilder<Autopark> builder)
	{
		builder
			.HasMany(a => a.ActualCars)
			.WithOne(ac => ac.ActualAutopark)
			.HasForeignKey(c => c.ActualAutoparkId)
			.OnDelete(DeleteBehavior.NoAction);

		builder
			.HasMany(a => a.ActualDrivers)
			.WithOne(ad => ad.ActualAutopark)
			.HasForeignKey(c => c.ActualAutoparkId)
			.OnDelete(DeleteBehavior.NoAction);

		builder
			.HasMany(a => a.ActualTrailers)
			.WithOne(at => at.ActualAutopark)
			.HasForeignKey(c => c.ActualAutoparkId)
			.OnDelete(DeleteBehavior.NoAction);

		builder
			.HasMany(a => a.ShipStarts)
			.WithOne(s => s.AutoparkStart)
			.HasForeignKey(s => s.AutoparkStartId)
			.OnDelete(DeleteBehavior.NoAction);
			
		builder
			.HasMany(a => a.ShipFinishes)
			.WithOne(s => s.AutoparkFinish)
			.HasForeignKey(s => s.AutoparkFinishId)
			.OnDelete(DeleteBehavior.NoAction);
	}

}