using Cargodi.DataAccess.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class DriverCategoryConfiguration : IEntityTypeConfiguration<DriverCategory>
{
    public void Configure(EntityTypeBuilder<DriverCategory> builder)
    {
        builder.HasKey(o => new { o.CategoryName, o.DriverId });

        builder
            .HasOne(o => o.Driver)
            .WithMany(d => d.DriverCategories)
            .HasForeignKey(o => o.DriverId);

        builder
            .HasOne(o => o.Category)
            .WithMany(c => c.DriverCategories)
            .HasForeignKey(o => o.CategoryName);
    }

}
