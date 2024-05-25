using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class CarSeeds : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        var cars = new List<Car>();
        
        for (int i = 1; i <= 10; i++)
        {
            var autoparkId = Generator.GenerateRandomNumber(1, 2);
            
            cars.Add(new Car
            {
                Id = i,
                LicenseNumber = Generator.GenerateLicenseNumber(),
                Mark = Generator.GenerateMark(),
                Range = Generator.GenerateRandomNumber(100, 1000),
                Carrying = Generator.GenerateRandomNumber(500_000, 7_500_000),
                TankVolume = Generator.GenerateRandomNumber(30, 100),
                CapacityLength = Generator.GenerateRandomNumber(1000, 7000),
                CapacityWidth = Generator.GenerateRandomNumber(1000, 3000),
                CapacityHeight = Generator.GenerateRandomNumber(1000, 3000),
                AutoparkId = autoparkId,
                ActualAutoparkId = autoparkId,
                CarTypeId = Generator.GenerateRandomNumber(1, 3)
            });
        }
        
        builder.HasData(cars);
    }
}