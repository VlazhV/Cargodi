using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class CarTypeCategoryConfiguration : IEntityTypeConfiguration<CarTypeCategory>
{
	public void Configure(EntityTypeBuilder<CarTypeCategory> builder)
	{
		builder
			.HasKey(c => new { c.CarTypeId, c.CategoryName });

		builder
			.HasOne(ct => ct.CarType)
			.WithMany(t => t.CarTypeCategories)
			.HasForeignKey(ct => ct.CarTypeId);
			
		builder
			.HasOne(ct => ct.Category)
			.WithMany(c => c.CarTypeCategories)
			.HasForeignKey(ct => ct.CategoryName);

		builder.HasData(
			new CarTypeCategory { CarTypeId = 1, CategoryName = "C" },
			new CarTypeCategory { CarTypeId = 1, CategoryName = "CE" },
			new CarTypeCategory { CarTypeId = 2, CategoryName = "C" },
			new CarTypeCategory { CarTypeId = 2, CategoryName = "B" },
			new CarTypeCategory { CarTypeId = 3, CategoryName = "B" },
			new CarTypeCategory { CarTypeId = 3, CategoryName = "BE" }
		);
	}
}