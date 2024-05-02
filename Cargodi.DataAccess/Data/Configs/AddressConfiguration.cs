using Cargodi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
	public void Configure(EntityTypeBuilder<Address> builder)
	{
		builder.HasMany(e => e.LoadOrders)
			.WithOne(e => e.LoadAddress)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasMany(e => e.DeliveryOrders)
			.WithOne(e => e.DeliverAddress)
			.OnDelete(DeleteBehavior.NoAction);
	}

}
