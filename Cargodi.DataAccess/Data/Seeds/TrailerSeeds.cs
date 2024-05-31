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
                Carrying = Generator.GenerateRandomNumber(500, 7_500),
                CapacityLength = Generator.GenerateRandomNumber(100, 700),
                CapacityWidth = Generator.GenerateRandomNumber(100, 300),
                CapacityHeight = Generator.GenerateRandomNumber(100, 300),
                AutoparkId = autoparkId,
                ActualAutoparkId = autoparkId,
                TrailerTypeId = Generator.GenerateRandomNumber(1, 3)
            });
        }

        builder.HasData(trailers);
    }

}