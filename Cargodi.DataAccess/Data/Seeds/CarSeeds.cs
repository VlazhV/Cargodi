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
                Carrying = Generator.GenerateRandomNumber(500, 7_500),
                TankVolume = Generator.GenerateRandomNumber(30, 100),
                CapacityLength = Generator.GenerateRandomNumber(100, 700),
                CapacityWidth = Generator.GenerateRandomNumber(100, 300),
                CapacityHeight = Generator.GenerateRandomNumber(100, 300),
                AutoparkId = autoparkId,
                ActualAutoparkId = autoparkId,
                CarTypeId = Generator.GenerateRandomNumber(1, 3)
            });
        }
        
        builder.HasData(cars);
    }
}