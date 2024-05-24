using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class TrailerTypeConfiguration : IEntityTypeConfiguration<TrailerType>
{
	public void Configure(EntityTypeBuilder<TrailerType> builder)
	{
		builder.HasData(
			TrailerTypes.Bulker,
			TrailerTypes.Cistern,
			TrailerTypes.VanTrailer
		);
	}

}