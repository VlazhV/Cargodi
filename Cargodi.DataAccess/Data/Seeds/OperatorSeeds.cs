using Cargodi.DataAccess.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class OperatorSeeds : IEntityTypeConfiguration<Operator>
{
    public void Configure(EntityTypeBuilder<Operator> builder)
    {
        var operators = new List<Operator>
        {
            new Operator
            {
                Id = 1,
                UserId = 1,
                FirstName = "Дмитрий",
                SecondName = "Попов",
                MiddleName = "Владимирович",
                AutoparkId = 1,
                FireDate = null,
                EmployDate = new DateTime(
                    Generator.GenerateRandomNumber(2020, 2023),
                    Generator.GenerateRandomNumber(1, 12),
                    Generator.GenerateRandomNumber(1, 28)
                )
            },
            new Operator
            {
                Id = 2,
                UserId = 2,
                FirstName = "Михаил",
                SecondName = "Шумахер",
                MiddleName = "Михаилович",
                AutoparkId = 1,
                FireDate = null,
                EmployDate = new DateTime(
                    Generator.GenerateRandomNumber(2020, 2023),
                    Generator.GenerateRandomNumber(1, 12),
                    Generator.GenerateRandomNumber(1, 28)
                )
            },
            new Operator
            {
                Id = 3,
                UserId = 3,
                FirstName = "Александра",
                SecondName = "Костюченко",
                MiddleName = "Ивановна",
                AutoparkId = 2,
                FireDate = null,
                EmployDate = new DateTime(
                    Generator.GenerateRandomNumber(2020, 2023),
                    Generator.GenerateRandomNumber(1, 12),
                    Generator.GenerateRandomNumber(1, 28)
                )
            },
        };

        builder.HasData(operators);
    }
}
