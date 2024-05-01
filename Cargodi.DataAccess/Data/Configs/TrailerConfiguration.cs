using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Data.Configs;

public class TrailerConfiguration : IEntityTypeConfiguration<Trailer>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Trailer> builder)
	{
		builder.HasIndex(trailer => trailer.LicenseNumber).IsUnique();
	}
}