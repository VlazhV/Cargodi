using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Data.Configs;

public class TrailerConfiguration : IEntityTypeConfiguration<Trailer>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Trailer> builder)
	{
		builder.HasIndex(trailer => trailer.LicenseNumber).IsUnique();

		builder.HasMany(e => e.Ships)
			.WithOne(e => e.Trailer)
			.OnDelete(DeleteBehavior.NoAction);
	}
}