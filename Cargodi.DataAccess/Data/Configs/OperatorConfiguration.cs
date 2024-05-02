using Cargodi.DataAccess.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class OperatorConfiguration : IEntityTypeConfiguration<Operator>
{
	public void Configure(EntityTypeBuilder<Operator> builder)
	{
		builder.HasMany(e => e.Orders)
			.WithOne(e => e.Operator)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(e => e.User)
			.WithOne(e => e.Operator)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(e => e.Autopark)
			.WithMany(e => e.Operators)
			.OnDelete(DeleteBehavior.NoAction);
	}

}