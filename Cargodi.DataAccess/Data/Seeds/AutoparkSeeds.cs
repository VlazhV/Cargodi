using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class AutoparkSeeds : IEntityTypeConfiguration<Autopark>
{
    public void Configure(EntityTypeBuilder<Autopark> builder)
    {
        var data = new List<Autopark>
        {
            new Autopark
            {
                Id = 1,
                AddressId = 1,
                Capacity = 250
            },
            new Autopark
            {
                Id = 2,
                AddressId = 2,
                Capacity = 200
            },
        };

        builder.HasData(data);
    }

}
