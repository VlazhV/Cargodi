using Cargodi.DataAccess.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class DriverCategorySeeds : IEntityTypeConfiguration<DriverCategory>
{
    public void Configure(EntityTypeBuilder<DriverCategory> builder)
    {
        var data = new List<DriverCategory>
        {
            new()
            {
                DriverId = 1,
                CategoryName = "BE"
            },
            new()
            {
                DriverId = 1,
                CategoryName = "B"
            },
            new()
            {
                DriverId = 1,
                CategoryName = "C"
            },

            new()
            {
                DriverId = 2,
                CategoryName = "B"
            },
            new()
            {
                DriverId = 2,
                CategoryName = "BE"
            },
            new()
            {
                DriverId = 2,
                CategoryName = "CE"
            },
        };

        builder.HasData(data);
    }

}
