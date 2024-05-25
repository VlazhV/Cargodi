using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class DriverSeeds : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        var drivers = new List<Driver>()
        {
            new Driver
            {
                Id = 1,
                FirstName = "Иванов",
                SecondName = "Иван",
                MiddleName = "Иванович",
                FireDate = null,
                EmployDate = new DateTime(
                    Generator.GenerateRandomNumber(2020, 2024),
                    Generator.GenerateRandomNumber(1, 12),
                    Generator.GenerateRandomNumber(1, 28)
                ),
                License = Generator.GenerateLicense(),
                UserId = 5,
                ActualAutoparkId = 1,
                AutoparkId = 1,
                DriverStatusId = DriverStatuses.Works.Id
            },
            new Driver
            {
                Id = 2,
                FirstName = "Василий",
                SecondName = "Васильев",
                MiddleName = "Васильевич",
                FireDate = null,
                EmployDate = new DateTime(
                    Generator.GenerateRandomNumber(2020, 2023),
                    Generator.GenerateRandomNumber(1, 12),
                    Generator.GenerateRandomNumber(1, 28)
                ),
                License = Generator.GenerateLicense(),
                UserId = 6,
                ActualAutoparkId = 2,
                AutoparkId = 2,
                DriverStatusId = DriverStatuses.Vacations.Id
            },
        };

        builder.HasData(drivers);
    }

}