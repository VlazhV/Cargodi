using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class TrailerSeeds : IEntityTypeConfiguration<Trailer>
{
    public void Configure(EntityTypeBuilder<Trailer> builder)
    {
        var trailers = new List<Trailer>();
        
        for (int i = 1; i <= 10; i++)
        {
            var autoparkId = Generator.GenerateRandomNumber(1, 2);

            trailers.Add(new Trailer
            {
                Id = i,
                LicenseNumber = Generator.GenerateLicenseNumber(),
                Carrying = Generator.GenerateRandomNumber(500_000, 7_500_000),
                CapacityLength = Generator.GenerateRandomNumber(1000, 7000),
                CapacityWidth = Generator.GenerateRandomNumber(1000, 3000),
                CapacityHeight = Generator.GenerateRandomNumber(1000, 3000),
                AutoparkId = autoparkId,
                ActualAutoparkId = autoparkId,
                TrailerTypeId = Generator.GenerateRandomNumber(1, 3)
            });
        }

        builder.HasData(trailers);
    }

}