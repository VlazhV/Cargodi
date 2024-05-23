using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class PayloadTypeConfiguration : IEntityTypeConfiguration<PayloadType>
{
	public void Configure(EntityTypeBuilder<PayloadType> builder)
	{
		builder.HasData(
			PayloadTypes.Bulk,
			PayloadTypes.Item,
			PayloadTypes.Liquid
		);
	}

}